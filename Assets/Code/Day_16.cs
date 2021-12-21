using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_16 : MonoBehaviour
{
    private static int versionSum = 0;

    private static int processPackagesSafety = 0;


    private void Start()
    {
        Part_1();

        /*
        //Test binary (base 2: 0-1) to decimal (base 10: 0-9) 
        string binary_2027 = "011111100101"; //Should be 2021
        string binary_1 = "001"; //Should be 1

        int decimal_2027 = Convert.ToInt32(binary_2027, fromBase: 2);
        int decimal_1 = Convert.ToInt32(binary_1, fromBase: 2);

        Debug.Log(decimal_2027);
        Debug.Log(decimal_1);
        */
    }

    

    private void Part_1()
    {
        //Get the data 
        string[] allRowsString = FileManagement.GetInputData("Day_16", "input.txt");

        string input_actual = allRowsString[0];

        //Debug.Log(allRowsString.Length);
        //Debug.Log(allRowsString[0]);

        //1 row with letters and numbers
        //Input is hexadecimal, so 0-9 and A-D

        //Step one is to convert hexadecimal to 4 bits of binary data
        // 0 -> 0000
        // A -> 1010

        //A BITS transmission includes a package which includes sub-packages which can include sub-packages in a hierarchy
        //If you convert the input "D2FE28" to: 110100101111111000101000
        //Then:
        // - The first 3 bits encode the packet "version:" 110 -> 6 in decimal 
        // - The next 3 bits encode the packet "type ID:" 100 -> 4 in decimal
        //If the "type ID" is 4, then the packet is a "literal value," meaning that we should find a single binary number:
        //After removing the version and ID, we have: 101111111000101000
        //The first groups always start with a 1, and the last group starts with a 0:
        //Each group always has a length of 5
        // - 10111 -> 0111 
        // - 11110 -> 1110
        // - 00101 -> 0101 (last group because starts with 0)
        // - 000 -> The last 3 bits are unlabeled and should be ignored
        //If we combine the bits, we get 011111100101 -> 2021 in decimal

        //There are also operator packets if the "type ID" is any other number than 4
        //These contain one or more packages, determined by a 1 or 0 after the "type ID"
        // - 0 -> The next 15 bits determines the length of the string that includes the number of sub-packages, but we don't know how many
        // - 1 -> The next 11 bits determines of many sub-pckages we have
        //After the 15 or 11 bits, the sub-packets appear
        //001 110 0 000000000011011 1101000101001010010001001000000000
        //Version: 001 -> 1
        //Type id: 110 -> 6 -> operator package
        //Length type ID: 0 -> 000000000011011 (15 bits) -> 27 -> the length of the sub-packets
        //Whats left is: 1101000101001010010001001000000000, but we know the length should be 27, so we can skip a few 0s, so we get:
        //110100010100101001000100100 0000000


        //Convert from hexadecimal to 4 bits of binary data
        //Should be: 110100101111111000101000
        //Output:    110100101111111000101000

        //string input_test = "D2FE28"; //should output lateral value 2021
        //string input_test = "38006F45291200"; //Operator packet with mode 0
        //string input_test = "EE00D40C823060"; //Operator packet with mode 1

        //string input_test = "8A004A801A8002F478"; //16
        //string input_test = "620080001611562C8802118E34"; //12 
        string input_test = "C0015000016115A2E0802F182340"; //23
        //string input_test = "A0016C880162017C3686B18A3D4780"; //31


        //Example: 8A004A801A8002F478

        //100 010 1 00000000001 001010100000000001101010000000000000101111010001111000
        // Parent package: Version: 4, ID: 2 -> mode: 1 -> next 11 determines how man sub-packages: 00000000001 -> 1 sub-package
        // Sub-package 1: 001 010 1 00000000001 101010000000000000101111010001111000
        // Version: 1, ID: 2 -> mode: 1 -> next 11 determines how man sub-packages: -> 00000000001 -> 1 sub-package
        // Sub-sub-package: 101 010 0 000000000001011 11010001111000
        // Version: 5, ID: 2 -> mode: 0 -> next 15 determines the length of the string with sub-packages -> 000000000001011 -> length 11
        // Sub-sub-sub-package: 110 100 01111 (the last three 0s were removed because we know the length is 11)
        // Version: 6, ID: 4 -> literal value = 15
        // Total version = 4 + 1 + 5 + 6 = 16

        //Example: 620080001611562C8802118E34
        //011 000 1 00000000010 00000000000000000101100001000101010110001011001000100000000010000100011000111000110100
        //v: 3, ID: 0 -> mode 1 -> next 11 determines how many sub-packages: 00000000010 -> 2 sub-packages

        //We know this includes two sub-packages
        //00000000000000000101100001000101010110001011001000100000000010000100011000111000110100

        //000 000 0 000000000010110 0001000101010110001011 001000100000000010000100011000111000110100
        //Sub-package 1: v: 0, ID: 0 -> mode 0: next 15 determines the length of the string with sub-packages -> 000000000010110 -> length 22
        //Sub-sub-sub-package from sub-package 1: 0001000101010110001011 
        // - value 1: 000 100 01010 
        // - value 2: 101 100 01011 

        //001 000 1 00000000010 000100011000111000110100
        //Sub-package 2: ID: 0 -> mode 1: next 11 determines how many sub-packages: 00000000010 -> 2 sub-packages
        //Sub-sub-sub-package from sub-package 2: 
        // - value 1: 000 100 01100 
        // - value 2: 011 100 01101 00

        // Total version: 3 + 1 + 


        //Example C0015000016115A2E0802F182340
        //110 000 0 000000001010100 000000000000000001011000010001010110100010111000001000000000101111000110000010001101 000000
        //v: 6, id: 0, mode: 0 -> find the next 15 bits: 000000001010100 -> 84
        //84 bits: 000000000000000001011000010001010110100010111000001000000000101111000110000010001101

        //000 000 0 000000000010110 0001000101011010001011 1000001000000000101111000110000010001101
        //v: 0, id: 0, mode: 0 -> find the next 15 bits: 000000000010110 = 22 

        //22 bits: 
        // - value 1: 000 100 01010 
        // - value 2: 110 100 01011

        //We still have 100 000 1 00000000010 1111000110000010001101
        //v: 4, id: 0, mode: 1 -> find the next 11 bits: 00000000010 = 2 packages 
        //Package 1: 
        // - value 1: 111 100 01100 
        // - value 2: 000 100 01101

        string binaryString = ConvertHexToBinary(input_test);

        Debug.Log(binaryString);

        //Assumer we cant have literal value and subpackage at the same time
        ProcessPackage(binaryString);


        Debug.Log($"Sum of version numbers: {versionSum}");
    }



    private void ProcessPackage(string binaryString)
    {
        processPackagesSafety += 1;

        if (processPackagesSafety > 10000000)
        {
            Debug.Log("Stuck in infinite loop when processing packages");

            return;
        }
    
    
        //The first 3+3 are always known
        int version = GetVersion(binaryString);
        int typeID = GetTypeID(binaryString);

        //Debug.Log($"Version: {version}");
        //Debug.Log($"Type ID: {typeID}");

        versionSum += version;

        Debug.Log(version);

        //Is this a literal value or do we have sub-packages
        if (typeID == 4)
        {
            int literalValue = CalculateLiteralValue(binaryString);

            //Debug.Log(literalValue);
        }
        //Subpackages so we have to continue to find their respective literal value
        else
        {
            //Now we are interested in the 7th number (the mode)
            int mode = GetMode(binaryString);

            //Debug.Log($"Mode: {mode}");

            if (mode == 0)
            {
                //Mode 0 means that the length that determines the length of the subpackages is 15
                string subPacketsLengthString = binaryString.Substring(7, 15);

                //Debug.Log(subPacketsLengthString);

                int subPacketsLength = Convert.ToInt32(subPacketsLengthString, fromBase: 2);

                //Debug.Log($"Sub packets length: {subPacketsLength}");

                string subPackages = binaryString.Substring(7 + 15, subPacketsLength);

                IdentifyAndProcessSubPackages(subPackages);
            }
            else if (mode == 1)
            {
                //Mode 1 means that the length that determines the length of the subpackages is 11
                string numberOfSubPackagesString = binaryString.Substring(7, 11);

                //Debug.Log(numberOfSubPackagesString);

                int numberOfSubPackages = Convert.ToInt32(numberOfSubPackagesString, fromBase: 2);

                //Debug.Log($"Number of sub-packages: {numberOfSubPackages}");

                string subPackages = binaryString.Substring(7 + 11);

                IdentifyAndProcessSubPackages(subPackages, numberOfSubPackages);
            }
        }
    }



    private int GetVersion(string binaryString)
    {
        string version_binary = binaryString.Substring(0, 3);

        int version = Convert.ToInt32(version_binary, fromBase: 2);

        return version;
    }



    private int GetTypeID(string binaryString)
    {
        string typeID_binary = binaryString.Substring(3, 3);

        int typeID = Convert.ToInt32(typeID_binary, fromBase: 2);

        return typeID;
    }



    private int GetMode(string binaryString)
    {
        string mode_binary = binaryString.Substring(6, 1);

        int mode = Convert.ToInt32(mode_binary, fromBase: 2);

        return mode;
    }



    private void IdentifyAndProcessSubPackages(string subPackages, int totalSubPackages)
    {
        //We can assume only the parent package is using extra zeroes
        int safety = 0;

        //List<string> allSubpackages = new List<string>();

        while (true)
        {
            safety += 1;

            if (safety > 10000000)
            {
                Debug.Log("Stuck in infinite loop when finding subpackages when knowing how many we have");

                break;
            }

            //Value or another subpackages
            int typeID = GetTypeID(subPackages);

            int version = GetVersion(subPackages);

            

            if (typeID == 4)
            {
                int length = GetLiteralValuePackageLength(subPackages);

                string thisPackage = subPackages.Substring(0, length);

                //ProcessPackage(thisPackage);

                //Cut the string
                //AAAAAAAAAAA BBBBBBBBBBBBBBBB

                subPackages = subPackages.Substring(length);

                ProcessPackage(thisPackage);
            }
            else
            {
                //Debug.Log(version);

                //versionSum += version;

                //Get the mode
                int mode = GetMode(subPackages);

                if (mode == 0)
                {
                    //Mode 0 means that the length that determines the length of the subpackages is 15
                    string subPacketsLengthString = subPackages.Substring(7, 15);

                    //Debug.Log(subPacketsLengthString);

                    int subPackagesLength = Convert.ToInt32(subPacketsLengthString, fromBase: 2);

                    //Debug.Log($"Sub packets length: {subPacketsLength}");

                    subPackages = subPackages.Substring(7 + 15, subPackagesLength);

                    IdentifyAndProcessSubPackages((string)subPackages.Clone());

                    //allSubpackages.Add((string)subPackages.Clone());
                }
                else if (mode == 1)
                {
                    //Mode 1 means that the length that determines the length of the subpackages is 11
                    string numberOfSubPackagesString = subPackages.Substring(7, 11);

                    //Debug.Log(numberOfSubPackagesString);

                    int numberOfSubPackages = Convert.ToInt32(numberOfSubPackagesString, fromBase: 2);

                    //Debug.Log($"Number of sub-packages: {numberOfSubPackages}");

                    subPackages = subPackages.Substring(7 + 11);

                    IdentifyAndProcessSubPackages((string)subPackages.Clone(), numberOfSubPackages);
                    //allSubpackages.Add((string)subPackages.Clone());
                }
            }


            if (safety == totalSubPackages)
            {
                break;
            }
        }


        //foreach (string s in allSubpackages)
        //{
        //    ProcessPackage(s);
        //}
    }


    private void IdentifyAndProcessSubPackages(string subPackages)
    {
        int safety = 0;

        while (true)
        {
            safety += 1;

            if (safety > 10000000)
            {
                Debug.Log("Stuck in infinite loop when finding subpackages from a string where we dont know how many we have");

                break;
            }

            //Value or another subpackages
            int typeID = GetTypeID(subPackages);

            int version = GetVersion(subPackages);

            //Debug.Log(version);

            versionSum += version;

            if (typeID == 4)
            {
                int length = GetLiteralValuePackageLength(subPackages);

                string thisPackage = subPackages.Substring(0, length);

                ProcessPackage(thisPackage);

                //Cut the string
                //AAAAAAAAAAA BBBBBBBBBBBBBBBB

                subPackages = subPackages.Substring(length);
            }
            else
            {
                //Debug.Log(version);

                //versionSum += version;

                //Get the mode
                int mode = GetMode(subPackages);

                if (mode == 0)
                {
                    //Mode 0 means that the length that determines the length of the subpackages is 15
                    string subPacketsLengthString = subPackages.Substring(7, 15);

                    //Debug.Log(subPacketsLengthString);

                    int subPacketsLength = Convert.ToInt32(subPacketsLengthString, fromBase: 2);

                    //Debug.Log($"Sub packets length: {subPacketsLength}");

                    subPackages = subPackages.Substring(7 + 15, subPacketsLength);

                    IdentifyAndProcessSubPackages((string)subPackages.Clone());
                }
                else if (mode == 1)
                {
                    //Mode 1 means that the length that determines the length of the subpackages is 11
                    string numberOfSubPackagesString = subPackages.Substring(7, 11);

                    //Debug.Log(numberOfSubPackagesString);

                    int numberOfSubPackages = Convert.ToInt32(numberOfSubPackagesString, fromBase: 2);

                    //Debug.Log($"Number of sub-packages: {numberOfSubPackages}");

                    subPackages = subPackages.Substring(7 + 11);

                    IdentifyAndProcessSubPackages((string)subPackages.Clone(), numberOfSubPackages);
                }
            }


            //This is not always true because we may have trailing 0s, or are we removing those before we get here?????
            if (subPackages.Length == 0)
            {
                break;
            }
        }
    }



    //From a string with binary numbers that may include several packages, get the literal value package length
    private int GetLiteralValuePackageLength(string subPackages)
    {
        //version + type id = 6
        int length = 6;
    
        //Skip the first 6 which are the version and ID
        for (int i = 6; i < subPackages.Length; i += 5)
        {
            string binaryGroup = subPackages.Substring(i, 5);

            //We dont care about the first character
            //binaryNumber += binaryGroup.Substring(1);

            length += 5;

            //First encounter of '0' means we should stop (cant be 0)
            if (binaryGroup[0] == '0')
            {
                break;
            }
        }

        return length;
    }



    //binaryString should include version and type ID
    private int CalculateLiteralValue(string binaryString)
    {
        string binaryNumber = "";
    
        //Skip the first 6 which are the version and ID
        for (int i = 6; i < binaryString.Length; i+= 5)
        {
            string binaryGroup = binaryString.Substring(i, 5);

            //We dont care about the first character
            binaryNumber += binaryGroup.Substring(1);

            //First encounter of '0' means we should stop (cant be 0)
            if (binaryGroup[0] == '0')
            {
                break;
            }
        }

        //Binary to decimal
        int decimalNumber = Convert.ToInt32(binaryNumber, fromBase: 2);

        return decimalNumber;
    }



    private string ConvertHexToBinary(string hexString)
    {
        //Define our own mapping
        Dictionary<char, string> hexCharacterToBinary = new Dictionary<char, string>
        {
            { '0', "0000" },
            { '1', "0001" },
            { '2', "0010" },
            { '3', "0011" },
            { '4', "0100" },
            { '5', "0101" },
            { '6', "0110" },
            { '7', "0111" },
            { '8', "1000" },
            { '9', "1001" },
            { 'A', "1010" },
            { 'B', "1011" },
            { 'C', "1100" },
            { 'D', "1101" },
            { 'E', "1110" },
            { 'F', "1111" }
        };


        string binaryString = "";

        foreach (char c in hexString)
        {
            binaryString += hexCharacterToBinary[c];
        }

        return binaryString;
    }
}
