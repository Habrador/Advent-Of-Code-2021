using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_14 : MonoBehaviour
{
    
    private void Start()
    {
        Part_1();
    }

    

    private void Part_1()
    {
        //Get the data 
        string[] allRowsString = FileManagement.GetInputData("Day_14", "input.txt");

        //Data consists of two sections separated by an empty row
        //Section 1: PPFCHPFNCKOKOSBVCFPP
        //Section 2: Rows of VC -> N

        string polymerTemplateString = "";
        List<string> pairInsertionRulesStrings = new List<string>();

        bool hasFoundSpace = false;

        foreach (string row in allRowsString)
        {
            if (row.Length == 0)
            {
                hasFoundSpace = true;
            }
            else if (!hasFoundSpace)
            {
                polymerTemplateString = row;
            }
            else
            {
                pairInsertionRulesStrings.Add(row);
            }
        }

        //Debug.Log(polymerTemplateString);
        //Debug.Log(pairInsertionRulesStrings[0]);
        //Debug.Log(pairInsertionRulesStrings[pairInsertionRulesStrings.Count - 1]);

        Dictionary<string, string> pairInsertionRules = new Dictionary<string, string>();

        foreach (string s in pairInsertionRulesStrings)
        {
            string[] separator = { "->" };

            string[] rules = s.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            //Important to Trim() to avoid all leading and trailing white-space characters!
            pairInsertionRules[rules[0].Trim()] = rules[1].Trim();
        }

        //foreach (KeyValuePair<string, string> pairs in pairInsertionRules)
        //{
        //    Debug.Log(pairs.Key + ": " + pairs.Value);
        //}

        //Debug.Log(pairInsertionRules["NN"]);


        //The rule AB -> C means that when elements A and B are adjacent, a C should be inserted between them
        //The template is NNCB, which has 3 pairs: NN, NC, CB, and with the rules
        // NN -> C: NCN  
        // NC -> B: NBC
        // CB -> H: CHB
        //After step 1, we get NCNBCHB

        int STEPS = 10;

        for (int i = 0; i < STEPS; i++)
        {
            List<string> newElements = new List<string>();

            for (int j = 0; j < polymerTemplateString.Length - 1; j++)
            {
                string element = polymerTemplateString.Substring(j, 2);
                //Debug.Log(element);
                string characterToInsert = pairInsertionRules[element];

                string newElement = element.Insert(1, characterToInsert);

                newElements.Add(newElement);
            }

            //Combine into one string
            string newPolymerTemplateString = "";

            for (int j = 0; j < newElements.Count; j++)
            {
                if (j == 0)
                {
                    newPolymerTemplateString += newElements[j];
                }
                else
                {
                    newPolymerTemplateString += newElements[j].Substring(1, 2);
                }
            }

            polymerTemplateString = newPolymerTemplateString;
        }

        Debug.Log($"Final template: {polymerTemplateString}");


        //
        // To get the final answer we have to count characters
        //

        Dictionary<char, int> characterOccurances = new Dictionary<char, int>();

        foreach (char c in polymerTemplateString)
        {
            if (!characterOccurances.ContainsKey(c))
            {
                characterOccurances[c] = 1;
            }
            else
            {
                characterOccurances[c] += 1;
            }
        }

        //Find the max and min occurances
        int max = -int.MaxValue;
        int min =  int.MaxValue;

        foreach (KeyValuePair<char, int> pair in characterOccurances)
        {
            if (pair.Value > max)
            {
                max = pair.Value;
            }
            if (pair.Value < min)
            {
                min = pair.Value;
            }
        }


        int finalAnswer = max - min;

        //Should be 2027
        Debug.Log($"Final answer: {finalAnswer}");
    }
}
