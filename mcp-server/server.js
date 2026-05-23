#!/usr/bin/env node

import { McpServer } from "@modelcontextprotocol/sdk/server/mcp.js";
import { StdioServerTransport } from "@modelcontextprotocol/sdk/server/stdio.js";
import { z } from "zod";
import { exec } from "child_process";
import path from "path";

const PROJECT_DIR = "/Users/rajitha/Development/DailyDashboard"; // change this

const server = new McpServer({
  name: "local-engineering-mcp",
  version: "1.0.0"
});

function runCommand(command, cwd = PROJECT_DIR) {
  return new Promise((resolve) => {
    exec(command, { cwd }, (error, stdout, stderr) => {
      resolve({
        content: [
          {
            type: "text",
            text: error
              ? `ERROR:\n${stderr || error.message}`
              : stdout || stderr || "Command completed."
          }
        ]
      });
    });
  });
}

server.tool("health_check", "Check MCP server status", {}, async () => {
  return {
    content: [{ type: "text", text: "running" }]
  };
});

server.tool("get_current_directory", "Get MCP server working directory", {}, async () => {
  return {
    content: [{ type: "text", text: process.cwd() }]
  };
});

server.tool("daily_dashboard_git_status", "Check Git status", {}, async () => {
  return runCommand("git status --short");
});

server.tool("daily_dashboard_build", "Build DailyDashboard project", {}, async () => {
  return runCommand("dotnet build");
});

server.tool("daily_dashboard_test", "Run DailyDashboard tests", {}, async () => {
  return runCommand("dotnet test");
});

server.tool("daily_dashboard_build_and_test", "Build and test DailyDashboard project", {}, async () => {
  return runCommand("dotnet build && dotnet test");
});

server.tool(
  "daily_dashboard_search",
  "Search text inside DailyDashboard project",
  {
    keyword: z.string()
  },
  async ({ keyword }) => {
    return runCommand(`grep -R "${keyword}" . --exclude-dir=node_modules --exclude-dir=bin --exclude-dir=obj`);
  }
);

const transport = new StdioServerTransport();
console.log("DailyDashboard MCP server running...");
await server.connect(transport);