#! /bin/sh

# Script to build Unity Web project

project="306_project2"

echo "Attempting to build $project for Web"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile $(pwd)/unity.log \
  -projectPath $(pwd) \
  -buildWebPlayer "$(pwd)/Build/web/$project.exe" \
  -quit
  
echo 'Logs from build'
cat $(pwd)/unity.log