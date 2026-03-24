# Package Template - Documentation

## Summary
- Overview
- Features
- Simple Code Explanations
- Master Links

## Overview
PackageTemplate is a minimilistic but complete package base intended to be used as a base for smaller packages or improved package templates / structures. It can be installed by loading it from the disk in the Package Manager menu or by directly importing it into a Unity Project's Package folder. 

## Features
- A Runtime Component Example / Base 
    (`Runtime/RuntimeExample`)  
- An Editor Script Example / Base 
    (`Editor/EditorExample`)  
- Automated Tests For EditMode And PlayMode 
    (`Tests/Runtime/RuntimeTestExample` / `Tests/Editor/EditorTestExample`)
- A Sample Scene Showing Off Implementation 
    (`Samples/SampleScene/PackageTemplateSampleScene`) 


## Code

### Runtime Example
There are only 4 scripts in this template and they all revolve around `RuntimeExample`. 
public class RuntimeExample : MonoBehaviour
{
    [SerializeField] public string example = "Example Value";
    public void Start()
    {
        Debug.Log(example);
    }
}

RuntimeExample is extremely simple foundation :
it's a component that takes and prints a single string variable. RuntimeExample's Start() function is important because it serves as the example method to validate the Editor and Tests are working. 

### Editor Example
[CustomEditor(typeof(RuntimeExample))]
public class EditorExample : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        RuntimeExample subject = (RuntimeExample)target;
        if (GUILayout.Button("Print Example Message"))
        {
            Debug.Log(subject.example);
        }
    }
}

EditorExample takes the RuntimeExample foundation and add's a button to it's inspector / editor. When pressed the button will log whatever the RuntimeExample's example value string variable is. The sample scene comes with a prefab that shows off the button and it's functionality to help show how the editor links to the component. 

### Tests
public class EditModeExampleTest
{
    [Test] // Edit Mode Test
    public void TestRuntimeExampleEditMode()
    {
        // Creating Game Object and Runtime Script : 
        GameObject testGameObject = new GameObject();
        RuntimeExample runtimeExampleScript = testGameObject.AddComponent<RuntimeExample>();
        
        // Testing Runtime Script : 
        runtimeExampleScript.Start();
        Assert.IsNotNull(runtimeExampleScript);
        Debug.Log("Test Finished");
    }
}
public class RuntimeTestExample
{
    [UnityTest] // Play Mode Tests
    public IEnumerator TestRuntimeExamplePlayMode()
    {
        // Create the object and Script : 
        GameObject testGameObject = new GameObject();
        RuntimeExample runtimeExampleScript = testGameObject.AddComponent<RuntimeExample>();
        
        // Run the Script : 
        runtimeExampleScript.Start();
        yield return null;

        // Cleanup :
        Assert.IsNotNull(runtimeExampleScript);
        Object.Destroy(testGameObject);
        Debug.Log("Test Finished");
    }
}

There are two tests in this template, an Edit Mode Test `[Test]` and a Play Mode Test `[UnityTest]`
Both of them do the same thing: They create a game object, attach a RuntimeExample script then call the Start() method of that script. However the Edit Mode test can be run in the editor and thus is limited by what can be done in the Editor only whilst the play mode test will create a temporary scene instance and then run the test as if it was normal play. 

## Master Links
This pacakge template aims to be provide the structure / layout for all fundemantal package elements whilst being bare and minimilistic, I'm hoping to use it as a building base for many small projects in the future but it could definitely be expanded upon and/or improved. If you would like to add to my template or build a better one from scratch here are all the resources I found useful whilst developing this. 

[Unity Package Youtube Tutorial](https://youtu.be/mgsLb3TKljk?si=hM2qi7mbiC8Qhbca)
[Unity Package Documentation](https://docs.unity3d.com/Manual/PackagesList.html)
[Unity Package Layout](https://docs.unity3d.com/6000.3/Documentation/Manual/cus-layout.html)
[Unity Package Creation](https://docs.unity3d.com/2020.3/Documentation/Manual/CustomPackages.html)
[Unity Package Manifest](https://docs.unity3d.com/2020.3/Documentation/Manual/upm-manifestPkg.html)

[Infallible Code Package Youtube Tutorial](https://www.youtube.com/watch?v=OhwP5TYgucM)
[LlamAcademy Namespaces Youtube Tutorial](https://www.youtube.com/watch?v=rmP8vAuuhoA)
[LlamAcademy Assembly Definition Youtube Tutorial](https://youtu.be/qprZHOPu2OI?si=Pz4Td5j5_BV4c4WE)

[Keep A Changelog Website](https://keepachangelog.com/en/1.1.0/)
[Semantic Versioning](https://semver.org/spec/v2.0.0.html)
[Markdown Syntax](https://markdownguide.offshoot.io/basic-syntax/)

[GitHub Repo](https://github.com/RyanEllisDale/PackageTemplate)

# Thank You For Reading 