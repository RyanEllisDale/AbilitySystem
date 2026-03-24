# Package Template

# Documentation Links
[Unity Package Documentation](https://docs.unity3d.com/Manual/PackagesList.html)\
[Unity Package Layout](https://docs.unity3d.com/6000.3/Documentation/Manual/cus-layout.html)\
[Unity Package Creation](https://docs.unity3d.com/2020.3/Documentation/Manual/CustomPackages.html)\
[Unity Package Manifest](https://docs.unity3d.com/2020.3/Documentation/Manual/upm-manifestPkg.html)

## Introduction
This is a miniaturised Unity Package that I created because I found myself constantly going back and forth between Unity's documentation, my own previous projects, and Youtube tutorials everytime I wanted to create a new package. Instead of doing all that I thought to make it easier and more consistent by designing this template to work off of. 

## Content
This package contains absolute bare basic example elements from all aspects of Unity Packages.
- A Runtime folder with an `RuntimeExample` script and a `Runtime Assembly Definition`
- An Editor folder with an EditorExample script and an `Editor Assembly Definition`
- A Runtime / PlayMode Tests folder with an `RuntimeTestExample` script and a `Runtime Test Assembly Definition`
- An Editor / EditMode Tests folder with an `EditorTestsExample` script and am `Editor Test Assembly Definition`
- A `PackageTemplateSampleScene` SampleScene containing a prefab using `RuntimeExample` 
- A documentation folder with the standard `documentation.md` layout
- A `package.json` manifest using the standard structure and linking back to me
- A default `Third Party Notices.md` file for legality security
- A `CHANGELOG.md` file explaining Unity's / Keep The Changelog's layout as well as an example
- This `README.md` <- You Are Here

## License
This template is released into the public domain using [CC0 1.0](https://creativecommons.org/publicdomain/zero/1.0/).  
You can use it freely in personal or commercial projects.

# Thank You For Reading 