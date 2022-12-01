var yargs = require("yargs/yargs")(process.argv.slice(2)).help();

function defineInputCommand() {
  let date = new Date();
  let day = date.getDate(),
    year = date.getFullYear();

  yargs.command(
    ["input [day] [year]", "download"],
    "Download input for selected day",
    (y) => {
      y.positional("day", {
        default: day,
        type: "number",
      });
      y.positional("year", {
        default: year,

        type: "number",
      });
      y.option("force", {
        alias: "f",
        describe: "Will override file if already exists",
        default: false,
        type: "string",
      });
    },
    (args) => doInput(args)
  );
}

function run() {
  yargs.argv;
}

defineInputCommand();
run();

function doInput(args) {
  console.log("doing input");
  console.log({ args });
}
