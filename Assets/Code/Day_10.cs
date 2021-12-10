using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_10 : MonoBehaviour
{
    
    private void Start()
    {
        Part_1();
    }

    

    private void Part_1()
    {
        //Get the data 
        string[] allRowsString = FileManagement.GetInputData("Day_10", "input.txt");

        //The data consists of characters like )]{ in rows (not comma separated and no spaces between)
        //90 rows
        //Forst row: {[[<[({{[<[[[[<>()]{[][]}][[<>[]]([])]]]{({[[]()]<()<>>}[[()()]<{}()>])<{<[]<>><{}{}>}[<()<>>{[]()}]>}
        //Debug.Log(allRowsString.Length);
        //Debug.Log(allRowsString[0].Length);
        //Debug.Log(allRowsString[0]);

        //The data consists of chunks, a single chunk opens with <, (, {, or [ and closes with the corresponding ], }, ), >
        //So each row consists of several chunks
        //() is a chunk but contains no other chunks
        //Some rows are incomplete
        //Some chunks are corrupted, meaning they can look like: (]
        
        string incompleteExample = "[({(<(())[]>[[{[]{<()<>>";

        string corruptedExample_1 = "{([(<{}[<>[]}>{[]{[(<()>"; //Expected ], but found }
        string corruptedExample_2 = "[[<[([]))< ([[{ }[[()]]]"; //Expected ], but found )
        string corruptedExample_3 = "[{[{({}]{}}([{[{{{}}([]"; //Expected ), but found ]
        string corruptedExample_4 = "[<(<(<(<{}))><([]([]()"; //Expected >, but found )

        //Ignore the incomplete lines
        //Stop at the first incorrect closing character on each corrupted line, save it

        //Find the closing character, such as ] and try to find its opening character [

        // {([(<{}[<>[]}>{[]{[(<()>
        // {} -> {([(<[<>[]}>{[]{[(<()>
        // <> -> {([(<[[]}>{[]{[(<()>
        // [] -> {([(<[}>{[]{[(<()>
        // [} -> corrupt!


        // [[<[([]))< ([[{ }[[()]]]
        // [] -> [[<[())< ([[{ }[[()]]]
        // () -> [[<[)< ([[{ }[[()]]]
        // [) -> corrupt!

        // [{[{({}]{}}([{[{{{}}([]
        // {} -> [{[{(]{}}([{[{{{}}([]
        // (] -> corrupt!

        // [<(<(<(<{}))><([]([]()
        // {} -> [<(<(<(<))><([]([]()
        // <) -> corrupt!

        List<char> closingCharacters = new List<char>(new char[] { '>', ')', '}', ']' });

        Dictionary<char, char> oppositeCharacters = new Dictionary<char, char>();

        oppositeCharacters['>'] = '<';
        oppositeCharacters[')'] = '(';
        oppositeCharacters['}'] = '{';
        oppositeCharacters[']'] = '[';

        oppositeCharacters['<'] = '>';
        oppositeCharacters['('] = ')';
        oppositeCharacters['{'] = '}';
        oppositeCharacters['['] = ']';

        List<char> incorrectClosingCharacters = new List<char>(); 

        foreach (string row in allRowsString)
        {
            string currentString = row;

            int safety = 0;

            bool continueSearching = true;

            while (continueSearching)
            {
                safety += 1;

                if (safety > 1000000000)
                {
                    Debug.Log("Stuck in finite loop");

                    break;
                }


                bool foundMatchingOrNonMatching = false;

                for (int i = 0; i < currentString.Length; i++)
                {
                    char thisChar = currentString[i];

                    //We have found a closing character
                    if (closingCharacters.Contains(thisChar))
                    {
                        char characterBefore = currentString[i - 1];

                        //The characters are matching
                        if (thisChar == oppositeCharacters[characterBefore])
                        {
                            //So remove the chunk
                            currentString = currentString.Remove(i - 1, 2);

                            foundMatchingOrNonMatching = true;

                            break;
                        }
                        //The characters are not matching
                        else
                        {
                            Debug.Log($"Found corrupted chunk! Expected: {oppositeCharacters[characterBefore]}, but found: {thisChar}");

                            continueSearching = false;

                            foundMatchingOrNonMatching = true;

                            incorrectClosingCharacters.Add(thisChar);

                            break;
                        }
                    }
                }


                //We have searched the entire string, so we have found an incomplete row and should stop
                if (!foundMatchingOrNonMatching)
                {
                    Debug.Log("Found incomplete row");

                    continueSearching = false;
                }
            }
        }


        //Final sum:
        // ): 3 points
        // ]: 57 points
        // }: 1197 points
        // >: 25137 points

        int finalSum = 0;
        
        foreach (char c in incorrectClosingCharacters)
        {
            if (c == ')')
            {
                finalSum += 3;
            }
            else if (c == ']')
            {
                finalSum += 57;
            }
            else if (c == '}')
            {
                finalSum += 1197;
            }
            else if (c == '>')
            {
                finalSum += 25137;
            }
        }

        //Should be 339411
        Debug.Log($"Final sum: {finalSum}");
    }
}
