import zipfile
import os

# Project root directory (current working directory)
project_dir = os.getcwd()
zip_filename = "UnityProject_Trimmed.zip"

# Folders to include
include_folders = [
    "Assets",
    "Packages",
    "ProjectSettings",
    "UserSettings",
    # "QCAR"  # Optional: your Vuforia-related data
]

# Files to include
include_files = [
    # "LICENSE",
    "Speedway Saga.sln",
    "Assembly-CSharp.csproj",
    "Assembly-CSharp-Editor.csproj",
    ".gitignore"
]

# Create zip
with zipfile.ZipFile(zip_filename, "w", zipfile.ZIP_DEFLATED) as zipf:
    # Add folders
    for folder in include_folders:
        folder_path = os.path.join(project_dir, folder)
        for root, _, files in os.walk(folder_path):
            for file in files:
                file_path = os.path.join(root, file)
                arcname = os.path.relpath(file_path, project_dir)
                zipf.write(file_path, arcname)

    # Add root files
    for file in include_files:
        file_path = os.path.join(project_dir, file)
        if os.path.isfile(file_path):
            zipf.write(file_path, os.path.basename(file_path))

print(f"\n✅ Zipped successfully as: {zip_filename}")
