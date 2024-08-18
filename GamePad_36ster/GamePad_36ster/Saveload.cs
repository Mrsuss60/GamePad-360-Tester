using System;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using GamePad_36ster;

public class SaveLoad
{
    private const string SaveFileName = "backcolors.xml";

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

        XmlSerializer serializer = new XmlSerializer(typeof(ColorData));
        using (StreamWriter writer = new StreamWriter(SaveFileName))
        {
            serializer.Serialize(writer, colorData);
        }
    }

    public static void LoadColors(Backcolors backcolors)
    {
        if (!File.Exists(SaveFileName))
        {
            return; 
        }

        XmlSerializer serializer = new XmlSerializer(typeof(ColorData));
        ColorData colorData;

        using (StreamReader reader = new StreamReader(SaveFileName))
        {
            colorData = (ColorData)serializer.Deserialize(reader);
        }

        Color topLeft = new Color(colorData.TopLeftR, colorData.TopLeftG, colorData.TopLeftB);
        Color topRight = new Color(colorData.TopRightR, colorData.TopRightG, colorData.TopRightB);
        Color bottom = new Color(colorData.BottomR, colorData.BottomG, colorData.BottomB);

        backcolors.SetColors(topLeft, topRight, bottom);
    }
}

namespace GamePad_36ster
    {
     class SerializableAttribute : Attribute
    {
    }
}
