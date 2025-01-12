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
            
            case "SpawnLocation":
            case "Part": {
                comp = gameObject.AddComponent<PartComponent>(true);

                var part = (PartComponent)comp;

                CFrame cf = (CFrame)instance.GetProperty("CFrame").Value;
                var size = (RbxlReader.DataTypes.Vector3)instance.GetProperty("size").Value;
                var rot = cf.ToEulerAngles();
                var color = (Color3)instance.GetProperty("Color3uint8").Value;
                var transparency = (float)instance.GetProperty("Transparency").Value;

                part.StudPosition = new(-cf.Position.X, cf.Position.Z, cf.Position.Y);
                part.StudSize = new(size.X, size.Z, size.Y);
                part.StudRotation = new(MathX.RadianToDegree(rot.Pitch), MathX.RadianToDegree(rot.Yaw), MathX.RadianToDegree(rot.Roll));
                part.BrickColor = Color.FromBytes((int)color.R, (int)color.G, (int)color.B, (int)((1 - transparency) * 255));
                part.Shape = (PartShape)(uint)instance.GetProperty("shape").Value;

                break;
            }

            case "PointLight": {
                comp = gameObject.AddComponent<RbxlPointLightComponent>();

                var light = (RbxlPointLightComponent)comp;
                light.Strength = (float)instance.GetProperty("Brightness").Value;
                light.Range = (float)instance.GetProperty("Range").Value;
                
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