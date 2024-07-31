using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Niantic.Lightship.AR.ObjectDetection;
using Niantic.Lightship.AR.Subsystems.ObjectDetection;
using Niantic.Lightship.AR.XRSubsystems;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectDetectionOutputManager : MonoBehaviour
{
    [SerializeField]
    private ARObjectDetectionManager _objectDetectionManager;

    [SerializeField]
    public TMP_Text _objectsDetectedText;

    //[SerializeField]
    //public GameObject _objectsDetectedBox;

    public string curr_fruit = "";
    private bool detected = false;

    private void Start()
    {
        _objectDetectionManager.enabled = true;
        _objectDetectionManager.MetadataInitialized += OnMetadataInitialized;
        _objectsDetectedText.text = "";
    }

    private void OnMetadataInitialized(ARObjectDetectionModelEventArgs args)
    {
        _objectDetectionManager.ObjectDetectionsUpdated += ObjectDetectionsUpdated;
    }

    private void ObjectDetectionsUpdated(ARObjectDetectionsUpdatedEventArgs args)
    {
        //Initialize our output string
        string resultString = curr_fruit;
        var result = args.Results;


        if (result == null)
        {
            Debug.Log("No results found.");
            return;
        }

        //Reset our results string
        resultString = curr_fruit;
        bool flag = true;

        //Iterate through our results
        for (int i = 0; i < result.Count; i++)
        {
            var detection = result[i];
            var categorizations = detection.GetConfidentCategorizations();
            if (categorizations.Count <= 0)
            {
                break;
            }

            //Sort our categorizations by highest confidence
            categorizations.Sort((a, b) => b.Confidence.CompareTo(a.Confidence));

            var food = categorizations[0].CategoryName;
            
            if (flag)
            {
                flag = false;
                if (food == "berry" && detected == false)
                {
                    resultString += $"\t\tFresa \n";
                    resultString += "Por cada 100 gramos:\n";
                    resultString += "Calorías:\t\t 32\n";
                    resultString += "Carbohidratos:\t 7g\n";
                    resultString += "Proteínas:\t\t 0.67g \n";
                    resultString += "Grasas:\t\t 0.3g \n";
                    resultString += "Azúcares:\t\t 4.89g \n";
                    _objectsDetectedText.margin = new Vector4(100, 0, 100, 0);
                    detected = true;
                    //curr_fruit = resultString;
                }
                else if (food == "apple" && detected == false)
                {
                    resultString += $"\t\tManzana\n";
                    resultString += "Por cada 100 gramos:\n";
                    resultString += "Calorías:\t\t 52 \n";
                    resultString += "Carbohidratos:\t 13g \n";
                    resultString += "Proteínas:\t\t 0.26g \n";
                    resultString += "Grasas:\t\t 0.17g \n";
                    resultString += "Azúcares:\t\t 10.39g\n";
                    //curr_fruit = resultString;
                    detected = true;
                }
                else if (food == "banana" && detected == false)
                {
                    resultString = "";
                    resultString += $"\t\tBanana\n";
                    resultString += "Por cada 100 gramos:\n";
                    resultString += "Calorías:\t\t 122 \n";
                    resultString += "Carbohidratos:\t 31.89g \n";
                    resultString += "Proteínas:\t\t 1.30g \n";
                    resultString += "Grasas:\t\t 0.37g \n";
                    resultString += "Azúcares:\t\t 15g \n";
                    //curr_fruit = resultString;
                    detected = true;
                }
                else
                {
                    //flag = true;
                }
                curr_fruit = resultString;
                
            }
  
        }

        _objectsDetectedText.text = resultString;

    }

    public void clearText()
    {
        _objectsDetectedText.text = "";
        curr_fruit = "";
        detected = false;
    }

    private void OnDestroy()
    {
        _objectDetectionManager.MetadataInitialized -= OnMetadataInitialized;
        _objectDetectionManager.ObjectDetectionsUpdated -= ObjectDetectionsUpdated;
    }
}
