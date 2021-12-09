using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_08 : MonoBehaviour
{
    
    private void Start()
    {
        //Part_1();

        Part_2();
    }

    

    private void Part_1()
    {
        GetData(out List<string[]> column_L, out List<string[]> column_R);



        //This is the display:
        //  aaaa
        // b    c
        // b    c
        //  dddd 
        // e    f
        // e    f
        //  gggg
        //...if all letters are turned on forming the number 8
        //You can turn off letters to render a number: to render a 1, only segments c and f would be turned on; the rest would be off
        //This is called "Seven-segment display" and is what digital clocks use to display numbers 0-9

        //The submarine has a 4 digit display, so it can display 4 numbers using the above system
        //BUT this system is broken, so the wires to each segment in each display is randomly connected
        //All wires to a single segment in a single display are the same!

        //In the input data we have 10 unique signals in the first column and right column represents the 4 digit display
        //Each row is an entry. Within an entry, the same wire/segment connections are used
        //Your job is to use the left column to figure out what the 4 numbers in the right column are 
        //Clues:
        //2-letter combination: 1
        //3-letter combination: 7
        //4-letter combination: 4
        //7-letter combination: 8
        //If you see bg you know you want to display a 1 because only number 1 has 2 segments (= 2 wires) so that means that b and g should be connected to c and f instead of b and g (we still dont know if c is connected to b or g)


        //In the output values, how many times do digits 1, 4, 7, or 8 appear?

        int numberCounter = 0;

        foreach (string[] output in column_R)
        {
            foreach (string s in output)
            {
                if (s.Length == 2 || s.Length == 3 || s.Length == 4 || s.Length == 7)
                {
                    numberCounter += 1;
                }
            }
        }

        Debug.Log($"Number of digits that are 1, 4, 7, 8: {numberCounter}");
    }



    private void Part_2()
    {
        GetData(out List<string[]> column_L, out List<string[]> column_R);

        //Test case: acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf
        //acedgfb: 8
        //cdfbe: 5
        //gcdfa: 2
        //fbcad: 3
        //dab: 7
        //cefabd: 9
        //cdfgeb: 6
        //eafb: 4
        //cagedb: 0
        //ab: 1

        // dddd
        //e    a
        //e    a
        // ffff
        //g    b
        //g    b
        // cccc

        //Then, the four digits of the output value can be decoded:

        //cdfeb: 5
        //fcadb: 3
        //cdfeb: 5
        //cdbaf: 3

        string[] testNumbers = { "acedgfb", "cdfbe", "gcdfa", "fbcad", "dab", "cefabd", "cdfgeb", "eafb", "cagedb", "ab" };

        //Convert to an easier data structure to work with
        List<UnknownNumber> numbers = new List<UnknownNumber>();

        Dictionary<int, UnknownNumber> numbersDictionary = new Dictionary<int, UnknownNumber>();

        foreach (string numberString in testNumbers)
        {
            UnknownNumber unknownNumber = new UnknownNumber(numberString);
        
            numbers.Add(unknownNumber);

            //We know 1, 4, 7, 8
            if (unknownNumber.correspondingNumber == 1)
            {
                numbersDictionary[1] = unknownNumber;
            }
            else if (unknownNumber.correspondingNumber == 4)
            {
                numbersDictionary[4] = unknownNumber;
            }
            else if (unknownNumber.correspondingNumber == 7)
            {
                numbersDictionary[7] = unknownNumber;
            }
            else if (unknownNumber.correspondingNumber == 8)
            {
                numbersDictionary[8] = unknownNumber;
            }
        }

        //Debug.Log(numbersDictionary.Count);

        SevenDigits sevenDigits = new SevenDigits();



        //Remove letters from the Lists in seven sigits that we know

        //1 ab
        UnknownNumber number_1 = numbersDictionary[1];

        sevenDigits.RemoveNot(number_1.charactersArray, new List<char>[] { sevenDigits.TR, sevenDigits.BR });

        sevenDigits.Remove(number_1.charactersArray, new List<char>[] { sevenDigits.T, sevenDigits.M, sevenDigits.B, sevenDigits.TL, sevenDigits.BL });


        //7 dab
        UnknownNumber number_7 = numbersDictionary[7];
        
        sevenDigits.RemoveNot(number_7.charactersArray, new List<char>[] { sevenDigits.TR, sevenDigits.BR, sevenDigits.T });

        sevenDigits.Remove(number_7.charactersArray, new List<char>[] { sevenDigits.M, sevenDigits.B, sevenDigits.TL, sevenDigits.BL });

        //The top array should now include just d
        //sevenDigits.T.Display();
        //sevenDigits.TR.Display();
        //sevenDigits.BR.Display();
        //sevenDigits.B.Display();


        //4 eafb
        UnknownNumber number_4 = numbersDictionary[4];

        sevenDigits.RemoveNot(number_4.charactersArray, new List<char>[] { sevenDigits.TL, sevenDigits.M, sevenDigits.TR, sevenDigits.BR });

        sevenDigits.Remove(number_4.charactersArray, new List<char>[] { sevenDigits.T, sevenDigits.B, sevenDigits.BL });

        //sevenDigits.Display();

        //All but the T List should now have 2 letters
        // T: d
        // M: e f
        // B: c g
        //TL: e f
        //TR: a b
        //BL: c g
        //BR: a b

        //Numbers left to figure out:
        //True answer, characters, digits

        //0: cagedb, 6
        //6: cdfgeb, 6
        //9: cefabd, 6

        //2: gcdfa, 5
        //3: fbcad, 5
        //5: cdfbe, 5


        //A letter combination of 6 can be numbers: 0, 6, 9
        //A letter combination of 5 can be numbers: 2, 3, 5

        //3 includes both TR and BR (2 and 5 includes just one), so we can identify the 3 by finding those two characters in a character combination with length 5 
        string number_3_string = "";

        foreach (string s in testNumbers)
        {
            if (s.Length == 5)
            {
                if (s.IndexOf(sevenDigits.BR[0]) != -1 && s.IndexOf(sevenDigits.BR[1]) != -1)
                {
                    number_3_string = s;

                    break;
                }
            }
        }

        //Debug.Log($"Number 3 is: {number_3_string}");

        UnknownNumber number_3 = new UnknownNumber(number_3_string);

        numbersDictionary[3] = number_3;

        sevenDigits.RemoveNot(number_3.charactersArray, new List<char>[] { sevenDigits.T, sevenDigits.TR, sevenDigits.BR, sevenDigits.B, sevenDigits.M });

        sevenDigits.Remove(number_3.charactersArray, new List<char>[] { sevenDigits.TL, sevenDigits.BL });

        //Now we have the following 
        // T: d
        // M: f
        // B: c
        //TL: e
        //TR: a b
        //BL: g
        //BR: a b

        //To figure out TR and BR we can again use 2 and 5
        //2 includes TR and BL
        //We now know BL
        //3 doesnt include BL so we dont have to worry about that one! 
        char BL = sevenDigits.BL[0];

        string number_2_string = "";

        foreach (string s in testNumbers)
        {
            if (s.Length == 5)
            {
                if (s.IndexOf(BL) != -1)
                {
                    number_2_string = s;

                    break;
                }
            }
        }

        UnknownNumber number_2 = new UnknownNumber(number_2_string);

        numbersDictionary[2] = number_2;

        sevenDigits.RemoveNot(number_2.charactersArray, new List<char>[] { sevenDigits.T, sevenDigits.TR, sevenDigits.M, sevenDigits.BL, sevenDigits.B });

        sevenDigits.Remove(number_2.charactersArray, new List<char>[] { sevenDigits.TL, sevenDigits.BR });

        //And that should be it!


       

        Debug.Log("Answer to the problem: ");
        sevenDigits.Display();
    }



    private List<string> GetStringsWithLength(string[] strings, int length)
    {
        List<string> results = new List<string>();

        foreach (string s in strings)
        {
            if (s.Length == length)
            {
                results.Add(s);
            }
        }

        return results;
    }



    public class SevenDigits
    {
        //This is the display:
        //  aaaa
        // b    c
        // b    c
        //  dddd 
        // e    f
        // e    f
        //  gggg

        public List<char> T = new List<char>(new char[]{ 'a', 'b', 'c', 'd', 'e', 'f', 'g' });
        public List<char> M = new List<char>(new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' });
        public List<char> B = new List<char>(new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' });
        public List<char> TL = new List<char>(new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' });
        public List<char> BL = new List<char>(new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' });
        public List<char> TR = new List<char>(new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' });
        public List<char> BR = new List<char>(new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' });


        //Remove all but these from the List
        public void RemoveNot(List<char> charactersToKeep, List<char> charactersList)
        {
            List<char> charactersToRemove = new List<char>();

            for (int i = 0; i < charactersList.Count; i++)
            {
                if (!charactersToKeep.Contains(charactersList[i]))
                {
                    charactersToRemove.Add(charactersList[i]);
                }
            }

            foreach (char c in charactersToRemove)
            {
                charactersList.Remove(c);
            }
        }

        public void RemoveNot(List<char> charactersToKeep, List<char>[] charactersListArray)
        {
            foreach (List<char> charactersList in charactersListArray)
            {
                RemoveNot(charactersToKeep, charactersList);
            }
        }

        public void Remove(List<char> charactersToRemove, List<char> charactersList)
        {
            List<char> charactersToRemoveAfterChecking = new List<char>();

            for (int i = 0; i < charactersList.Count; i++)
            {
                if (charactersToRemove.Contains(charactersList[i]))
                {
                    charactersToRemoveAfterChecking.Add(charactersList[i]);
                }
            }

            foreach (char c in charactersToRemoveAfterChecking)
            {
                charactersList.Remove(c);
            }
        }

        public void Remove(List<char> charactersToRemove, List<char>[] charactersListArray)
        {
            foreach (List<char> charactersList in charactersListArray)
            {
                Remove(charactersToRemove, charactersList);
            }
        }



        public void Display()
        {
            T.Display(" T:");
            M.Display(" M:");
            B.Display(" B:");
            TL.Display("TL:");
            TR.Display("TR:");
            BL.Display("BL:");
            BR.Display("BR:");
        }
    }




    public class UnknownNumber
    {
        //Which of the seven digits are on?
        //public bool isA, isB, isC, isD, isE, isF, isG;

        //Which number is this?
        public int correspondingNumber;

        public List<char> charactersArray;

        public UnknownNumber(string stringInput)
        {
            //foreach (char letter in stringInput)
            //{
            //    if (letter == 'a')
            //    {
            //        isA = true;
            //    }
            //    else if (letter == 'b')
            //    {
            //        isB = true;
            //    }
            //    else if (letter == 'c')
            //    {
            //        isC = true;
            //    }
            //    else if (letter == 'd')
            //    {
            //        isD = true;
            //    }
            //    else if (letter == 'e')
            //    {
            //        isE = true;
            //    }
            //    else if (letter == 'f')
            //    {
            //        isF = true;
            //    }
            //    else if (letter == 'g')
            //    {
            //        isG = true;
            //    }
            //}


            //Now we can also check if this is an 1, 4, 7, 8
            //2-letter combination: 1
            //3-letter combination: 7
            //4-letter combination: 4
            //7-letter combination: 8
            if (stringInput.Length == 2)
            {
                correspondingNumber = 1;
            }
            else if (stringInput.Length == 3)
            {
                correspondingNumber = 7;
            }
            else if (stringInput.Length == 4)
            {
                correspondingNumber = 4;
            }
            else if (stringInput.Length == 7)
            {
                correspondingNumber = 8;
            }


            //Save the characters
            charactersArray = new List<char>(stringInput.ToCharArray());
        }
    }



    private void GetData(out List<string[]> column_L, out List<string[]> column_R)
    {
        //Get the data 
        string[] allRowsString = FileManagement.GetInputData("Day_08", "input.txt");

        //Data consists of two columns separated by a | 
        //200 rows
        column_L = new List<string[]>();
        column_R = new List<string[]>();

        foreach (string row in allRowsString)
        {
            string[] columns = row.Split('|');

            //Test
            //Debug.Log(columns[0]);
            //Debug.Log(columns[1]);

            //break;

            //Each column consists of letter combinations (a-g) separated by " " (column 1 has 10 part-columns and column 2 has 4 part-columns)

            //Needs a separator array because using ' ' requires .Net 6.0
            string[] separator = { " " };

            string[] L = columns[0].Split(separator, StringSplitOptions.RemoveEmptyEntries);
            string[] R = columns[1].Split(separator, StringSplitOptions.RemoveEmptyEntries);

            column_L.Add(L);
            column_R.Add(R);

            //Test
            //Debug.Log(L.Length);
            //Debug.Log(L[0]);
            //Debug.Log(L[L.Length - 1]);

            //Debug.Log(R.Length);
            //Debug.Log(R[0]);
            //Debug.Log(R[R.Length - 1]);
        }

        //Debug.Log(column_L.Count);
        //Debug.Log(column_R.Count);
    }



    /*
       //A solution is to use brute force and test all possible combinations

       List<int[]> answerCombinations_6 = new List<int[]>();

       answerCombinations_6.Add(new int[] { 0, 6, 9 });
       answerCombinations_6.Add(new int[] { 0, 9, 6 });
       answerCombinations_6.Add(new int[] { 6, 0, 9 });
       answerCombinations_6.Add(new int[] { 6, 9, 0 });
       answerCombinations_6.Add(new int[] { 9, 0, 6 });
       answerCombinations_6.Add(new int[] { 9, 6, 0 });

       List<int[]> answerCombinations_5 = new List<int[]>();

       answerCombinations_6.Add(new int[] { 2, 3, 5 });
       answerCombinations_6.Add(new int[] { 2, 5, 3 });
       answerCombinations_6.Add(new int[] { 3, 2, 5 });
       answerCombinations_6.Add(new int[] { 3, 5, 2 });
       answerCombinations_6.Add(new int[] { 5, 2, 3 });
       answerCombinations_6.Add(new int[] { 5, 3, 2 });

       //We also need to keep track of which string belongs to which number in the answer-combinations
       List<string> stringCombinations_6 = GetStringsWithLength(testNumbers, 6);
       List<string> stringCombinations_5 = GetStringsWithLength(testNumbers, 5);

       //Debug
       //stringCombinations_5.Display();
       //stringCombinations_6.Display();
       */
}
