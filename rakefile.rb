COMPILE_TARGET = ENV['config'].nil? ? "Debug" : ENV['config'] # Keep this in sync w/ VS settings since Mono is case-sensitive
CLR_TOOLS_VERSION = "v4.0.30319"

buildsupportfiles = Dir["#{File.dirname(__FILE__)}/buildsupport/*.rb"]
raise "Run `git submodule update --init` to populate your buildsupport folder." unless buildsupportfiles.any?
buildsupportfiles.each { |ext| load ext }

include FileTest
require 'albacore'

def sh command
 system command
end

desc "**Default**, compiles and runs tests"
task :default => [:compile, :unit_test]

desc "Target used for the CI server"
task :ci => [:update_all_dependencies, :default]

desc "Compiles the app"
task :compile => [:restore_if_missing] do
  MSBuildRunner.compile :compilemode => COMPILE_TARGET, :solutionfile => 'src/Pizza.sln', :clrversion => CLR_TOOLS_VERSION
end

desc "Runs unit tests"
task :test => [:unit_test]

desc "Runs unit tests"
task :unit_test => :compile do
  runner = NUnitRunner.new :compilemode => COMPILE_TARGET, :source => 'src', :platform => 'x86'
  runner.executeTests ['PizzaWeb.Test']
  runner.executeTests ['InteractionTests']
end