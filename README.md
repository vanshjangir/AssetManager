# ASSET MANAGER for GODOT

## Overview

The Godot Asset Manager  is a project designed to simplify the management of assets within the Godot game engine using C#. This asset manager leverages Selenium WebDriver and ChromeDriver to interact with the Godot Engine's web-based interface, enabling efficient asset management for your Godot projects.

## Features

- **Asset Uploading**: Easily upload assets to your Godot project directly through the web interface.
- **Asset Organization**: Manage your assets by creating folders and organizing them for better project structure.
- **Batch Operations**: Perform batch operations such as deleting, renaming, or moving multiple assets at once.
- **Asset Preview**: View asset previews before making changes or additions to your project.
- **User-Friendly**: A user-friendly interface makes it easy for both beginners and experienced users to work with assets.

## Requirements

Before you can use the Godot Asset Manager (C#), make sure you have the following prerequisites in place:

- [Godot Engine](https://godotengine.org/) installed on your machine.
- [C# support](https://docs.godotengine.org/en/stable/getting_started/scripting/c_sharp/index.html) configured within your Godot project.
- [Selenium WebDriver](https://www.selenium.dev/documentation/en/webdriver/driver_requirements/#quick-reference) installed.
- [ChromeDriver](https://sites.google.com/chromium.org/driver/) installed and available in your system's PATH.
- NOTE: We have already added it into the repository u don't need to install it additionally.
- A web browser (Google Chrome) installed on your machine.

## Getting Started

1. Clone or download this repository to your local machine.
2. Navigate to the project directory.

3. Open the Godot project that you want to manage assets for and ensure that C# support is enabled.

4. Create a C# script to integrate with the Godot Asset Manager. For example, you can create a new C# script called `AssetManagerIntegration.cs`.

5. In the C# script, import and use the `OpenQA.Selenium` namespace to interact with Selenium WebDriver.

6. Use the `AssetManagerIntegration.cs` script to invoke the Godot Asset Manager's methods for asset management, such as uploading, organizing, and performing batch operations.

7. Run your Godot project, and the C# script should interact with the Godot Asset Manager as needed.

## Usage

1. **Asset Upload**:
   - From your Godot project, trigger the asset upload process using the C# script by calling the appropriate methods in the `AssetManagerIntegration.cs` script.

2. **Asset Organization**:
   - Create folders and manage asset organization through the C# script by invoking relevant methods in `AssetManagerIntegration.cs`.

3. **Batch Operations**:
   - Implement batch operations for assets by using the provided methods in the `AssetManagerIntegration.cs` script.

4. **Asset Preview**:
   - Display asset previews within your Godot project's user interface based on user interactions with the C# script.

## Contributing

Contributions to the Godot Asset Manager project are welcome. If you have bug fixes, new features, or improvements to suggest, please fork this repository, make your changes, and submit a pull request.


## Acknowledgments

- Thanks to the Godot community for creating an incredible game engine.
- Selenium WebDriver and ChromeDriver for making web automation easier.
- Contributors to this project for their valuable input and contributions.

---

Enjoy using the Godot Asset Manager for efficient asset management in your Godot projects! If you encounter any issues or have suggestions for improvements, please don't hesitate to open an issue or reach out to us.
