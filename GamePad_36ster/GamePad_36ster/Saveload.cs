using System;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using GamePad_36ster;

public class SaveLoad
{
    private const string SaveFileName = "BackGroundColors.xml";

    [Serializable]
    public class ColorData
    {
        public float TopLeftR { get; set; }
        public float TopLeftG { get; set; }
        public float TopLeftB { get; set; }
        public float TopRightR { get; set; }
        public float TopRightG { get; set; }
        public float TopRightB { get; set; }
        public float BottomR { get; set; }
        public float BottomG { get; set; }
        public float BottomB { get; set; }
    }

    public static void SaveColors(Backcolors backcolors)
    {
        Color topLeft, topRight, bottom;
        backcolors.SetupGradient(out topLeft, out topRight, out bottom);

        ColorData colorData = new ColorData
        {
            TopLeftR = topLeft.R / 255f,
            TopLeftG = topLeft.G / 255f,
            TopLeftB = topLeft.B / 255f,
            TopRightR = topRight.R / 255f,
            TopRightG = topRight.G / 255f,
            TopRightB = topRight.B / 255f,
            BottomR = bottom.R / 255f,
            BottomG = bottom.G / 255f,
            BottomB = bottom.B / 255f
        };

        string rootDir = "GAMEP360";
        if (!Directory.Exists(rootDir))
            Directory.CreateDirectory(rootDir);

        string filePath = Path.Combine(rootDir, "BackGroundColors.xml");

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ColorData));
            serializer.Serialize(writer, colorData);
        }
    }

    public static void LoadColors(Backcolors backcolors)
    {
        string filepath = Path.Combine("GAMEP360", "BackGroundColors.xml");
        if (!File.Exists(filepath))
        {
            return;
        }
        using (StreamReader reader = new StreamReader(filepath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ColorData));
            ColorData colorData = (ColorData)serializer.Deserialize(reader);
            Color topLeft = new Color(colorData.TopLeftR, colorData.TopLeftG, colorData.TopLeftB);
            Color topRight = new Color(colorData.TopRightR, colorData.TopRightG, colorData.TopRightB);
            Color bottom = new Color(colorData.BottomR, colorData.BottomG, colorData.BottomB);

            backcolors.SetColors(topLeft, topRight, bottom);
        }
    }
}

namespace GamePad_36ster
{
    class SerializableAttribute : Attribute
    {
    }
}