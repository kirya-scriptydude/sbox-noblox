using Editor;
using Sandbox;
using RbxlReader;
using RbxlReader.Instances;
using System.Linq;
using RbxlReader.DataTypes;

public static class RbxlToSbox {

    public static void ImportFile(string filePath, Scene scene) {
        PlaceBinary place = new(filePath);

        foreach (GameObject obj in scene.Children) {
            if (obj.Tags.Has("rbxl")) {
                Log.Info("Found old .rbxl import object. Rewritting...");
                obj.Destroy();
            }
        }

        GameObject root = scene.CreateObject();
        root.Name = "Root";
        root.Tags.Add("rbxl");
        recursiveInstanceHandler(place.Workspace, scene, root);
    }

    private static void recursiveInstanceHandler(Instance parent, Scene scene, GameObject previousObj) {
        Instance[] instances = parent.GetChildren();
        if (instances.Length == 0) return;

        foreach(Instance instance in instances) {
            var gameObj = scene.CreateObject();
            gameObj.Parent = previousObj;
            handleProperties(instance, gameObj);

            recursiveInstanceHandler(instance, scene, gameObj);
        }
    }

    private static void handleProperties(Instance instance, GameObject gameObject) {
        InstanceComponent comp;
        switch (instance.ClassName) {

            case "Part": {
                comp = gameObject.AddComponent<PartComponent>(true);

                var part = (PartComponent)comp;

                CFrame cf = (CFrame)instance.GetProperty("CFrame").Value;
                var size = (RbxlReader.DataTypes.Vector3)instance.GetProperty("size").Value;
                var rot = cf.ToEulerAngles();

                part.StudPosition = new(cf.Position.X, cf.Position.Z, cf.Position.Y);
                part.StudSize = new(size.X, size.Z, size.Y);
                part.StudRotation = new(rot.Pitch, rot.Yaw, rot.Roll);
                //rotation is broken for the time being
                break;
            }

            default: {
                comp = gameObject.AddComponent<InstanceComponent>();
                break;
            }

        }
        comp.InstanceId = instance.Id;
        gameObject.Name = instance.Name;
        comp.ClassName = instance.ClassName;

        comp.ApplyData();
    }
}