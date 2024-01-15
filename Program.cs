namespace Programmierung1_uebungsblatt4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // TESTS.
            // 1.
            Console.WriteLine(addTwoBinaries("0101", "110010111011"));

            // 2.
            printDecimalEquivalentOfBinary("111010101000111001101");

            // 3. 
            printProductOfTwoBinaries("11111", "1010");

            // 4-7.
            Console.WriteLine(isMatrixSquare(createMatrix(5, 4)));
            printMatrixByRows(fillMatrixRandom(createMatrix(5, 5)));

            // 8.
            // Summate.
            var addend1 = fillMatrixRandom(createMatrix(2, 2));
            var addend2 = fillMatrixRandom(createMatrix(2, 2));
            printMatrixByRows(addend1);
            Console.WriteLine();
            printMatrixByRows(addend2);
            Console.WriteLine();
            printMatrixByRows(summateMatrices(fillMatrixRandom(createMatrix(2, 2)), fillMatrixRandom(createMatrix(2, 2))));

            // Multiply.
            var factor1 = fillMatrixRandom(createMatrix(3, 3));
            var factor2 = fillMatrixRandom(createMatrix(3, 2));
            printMatrixByRows(factor1);
            Console.WriteLine();
            printMatrixByRows(factor2);
            Console.WriteLine();
            printMatrixByRows(multiplyMatrices(factor1, factor2));

            // 9.
            Console.WriteLine(convertFloatToNumeral("1235678.12345678901"));

            // 10.
            Console.WriteLine(convertNumeralToRationalNum("dfghdfg"));
            Console.WriteLine(convertNumeralToRationalNum("elfmillioneneintausendvierundvierzig"));
            Console.WriteLine(convertNumeralToRationalNum("vierunddreißigtausendsechs"));
            Console.WriteLine(convertNumeralToRationalNum("einemillionachthundertfuenfundzwanzigtausendachthunderteins Vierhundertneuntel"));
            Console.WriteLine(convertNumeralToRationalNum("einemillionachthundertfuenfundzwanzigtausendachthunderteinskommanullsiebenachtvier"));

            // 11.
            printPascalsTriangle(8);

            // 12.
            printChristmasTree(7);
        }


        // FUNCTIONS.
        // 1.
        public static string addTwoBinaries(string binary1, string binary2)
        {
            // Pad with zeros for equal length.
            if (binary1.Length > binary2.Length)
            {
                binary2 = binary2.PadLeft(binary1.Length, '0');
            }
            if (binary2.Length > binary1.Length)
            {
                binary1 = binary1.PadLeft(binary2.Length, '0');
            }

            // Addition of binaries.
            string sum = "";
            int sumTemp;
            int transfer = 0;
            int indexBinary = binary1.Length - 1;

            while (indexBinary >= 0)
            {
                // Unicode equivalent to 1 = 49; 0 = 48,
                // subtraction necessary after casting.
                // TODO find better solution.
                sumTemp = ((int)binary1[indexBinary] - 48) +
                          ((int)binary2[indexBinary] - 48) + transfer;

                if (sumTemp == 0)
                {
                    sum = "0" + sum;
                    transfer = 0;
                }
                if (sumTemp == 1)
                {
                    sum = "1" + sum;
                    transfer = 0;
                }
                if (sumTemp == 2)
                {
                    sum = "0" + sum;
                    transfer = 1;
                }
                if (sumTemp == 3)
                {
                    sum = "1" + sum;
                    transfer = 1;
                }
                indexBinary--;
            }

            // Add remaining transfer to sum.
            if (transfer == 1) sum = transfer + sum;

            return sum;
        }


        // 2.
        public static void printDecimalEquivalentOfBinary(string binary)
        {
            int indexBinary = binary.Length - 1;
            int multiplier = 1;
            int decimalEquivalent = 0;

            while (indexBinary >= 0)
            {
                decimalEquivalent += ((int)binary[indexBinary] - 48) * multiplier;

                multiplier += multiplier;
                indexBinary--;
            }

            Console.WriteLine(binary);
            Console.WriteLine(decimalEquivalent);
        }


        // 3.
        public static void printProductOfTwoBinaries(string binary1, string binary2)
        {
            // Cache summated bits.
            int lengthCache = binary1.Length + binary2.Length - 1;
            int[] cachedBinaryChain = new int[lengthCache];

            for (int a = 0; a < binary2.Length; a++)
            {
                for (int b = binary1.Length - 1; b >= 0; b--)
                {
                    cachedBinaryChain[a + b] += ((int)binary1[b] - 48) *
                    ((int)binary2[a] - 48);
                }
            }

            // Translate cache to product.
            string product = "";
            int transfer = 0;

            for (int a = lengthCache - 1; a >= 0; a--)
            {
                int bitsInChain = cachedBinaryChain[a] + transfer;
                transfer = 0;

                // 0.
                if (bitsInChain == 0)
                {
                    product = bitsInChain.ToString() + product;
                }
                // 1.
                if (bitsInChain == 1)
                {
                    product = bitsInChain.ToString() + product;
                    continue;
                }
                // Even > 1.
                if (bitsInChain % 2 == 0 && bitsInChain != 0)
                {
                    product = "0" + product;
                    transfer = bitsInChain / 2;
                }
                // Odd > 2.
                if (bitsInChain % 2 == 1)
                {
                    product = "1" + product;
                    transfer = (bitsInChain - 1) / 2;
                }
            }

            // Add remaining transfer to product.
            if (transfer == 1) product = transfer + product;

            printDecimalEquivalentOfBinary(product);
        }


        // 4.
        public static int[,] createMatrix(int rows, int columns)
        {
            return new int[rows, columns];
        }


        // 5.
        public static int[,] fillMatrixRandom(int[,] matrix, int minRand = -10, int maxRand = 10)
        {
            var rand = new Random();
            //Thread.Sleep(200);

            for (int a = 0; a < matrix.GetLength(0); a++)
            {
                for (int b = 0; b < matrix.GetLength(1); b++)
                {
                    matrix[a, b] = rand.Next(minRand, maxRand);
                }
            }
            return matrix;
        }


        // 6.
        public static void printMatrixByRows(int[,] matrix)
        {
            for (int a = 0; a < matrix.GetLength(0); a++)
            {
                Console.Write("( ");

                for (int b = 0; b < matrix.GetLength(1); b++)
                {
                    Console.Write(matrix[a, b] + " ");
                }
                Console.Write(")");
                Console.WriteLine();
            }
        }


        // 7.
        public static bool isMatrixSquare(int[,] matrix)
        {
            bool isSquare = false;

            return isSquare = (matrix.GetLength(0) == matrix.GetLength(1)) ? true : false;
        }


        // 8.
        public static int[,] summateMatrices(int[,] addend1, int[,] addend2)
        {
            int numRowsAddend1 = addend1.GetLength(0);
            int numColsAddend1 = addend1.GetLength(1);

            int[,] sumMatrix = new int[numRowsAddend1, numColsAddend1];

            if (numRowsAddend1 == addend2.GetLength(0) &&
                numColsAddend1 == addend2.GetLength(1))
            {
                for (int a = 0; a < numColsAddend1; a++)
                {
                    for (int b = 0; b < numRowsAddend1; b++)
                    {
                        sumMatrix[a, b] = addend1[a, b] + addend2[a, b];
                    }
                }
            }
            return sumMatrix;
        }


        public static int[,] multiplyMatrices(int[,] factor1, int[,] factor2)
        {
            int numRowsFactor1 = factor1.GetLength(0);
            int numColsFactor2 = factor2.GetLength(1);

            int[,] productMatrix = new int[numRowsFactor1, numColsFactor2];

            if (factor1.GetLength(1) == factor2.GetLength(0))
            {
                for (int a = 0; a < numRowsFactor1; a++)
                {
                    for (int b = 0; b < numColsFactor2; b++)
                    {
                        for (int c = 0; c < numRowsFactor1; c++)
                        {
                            productMatrix[a, b] += factor1[a, c] * factor2[c, b];
                        }
                    }
                }
            }
            return productMatrix;
        }


        // 9.
        public static string convertFloatToNumeral(string inputFloat, string outputNumeral = "", int counterThousands = 0)
        {
            // Assign numerals to digits.
            Dictionary<char, string> numeralEquivOfDigit = new Dictionary<char, string>
        {
            { '1', "ein" },
            { '2', "zwei" },
            { '3', "drei" },
            { '4', "vier" },
            { '5', "fuenf" },
            { '6', "sechs" },
            { '7', "sieben" },
            { '8', "acht" },
            { '9', "neun" },
            { '0', "" }
        };

            // Split if has decimals.
            bool hasDecimals = false;
            inputFloat = inputFloat.Contains(".") ? inputFloat.Replace(".", ",") : inputFloat;
            string[] floatSplittedAtDecimals = new string[2];

            if (inputFloat.Contains(","))
            {
                floatSplittedAtDecimals = inputFloat.Split(',');
                inputFloat = floatSplittedAtDecimals[0];
                hasDecimals = true;
            }

            // For each thousand separator in inputFloat.
            int lengthInputFloat = inputFloat.Length;
            // 0.
            if (inputFloat[0] == '0' && lengthInputFloat == 1) return "null" + outputNumeral;

            char lastDigit = ' ';
            for (int a = 0; a < lengthInputFloat; a++)
            {
                if (a == 3) break;

                // Get numeral by digit.
                char currentDigit = inputFloat[lengthInputFloat - 1 - a];
                string numeral = numeralEquivOfDigit[currentDigit];

                // Build outputNumeral whole number part.
                switch (a)
                {
                    case 0:
                        // 2 - 9.
                        outputNumeral = numeral + outputNumeral;
                        lastDigit = currentDigit;
                        break;
                    case 1:
                        // 10.
                        if (currentDigit == '1' && lastDigit == '0') outputNumeral = "zehn";
                        // 11.					
                        if (currentDigit == '1' && lastDigit == '1') outputNumeral = "elf";
                        // 12.
                        if (currentDigit == '1' && lastDigit == '2') outputNumeral = "zwoelf";
                        // 13 -19.
                        if (currentDigit == '1' && (int)lastDigit - 48 >= 3) outputNumeral += "zehn";
                        // 20 - 29.					
                        if (currentDigit == '2') outputNumeral += "undzwanzig";
                        // 30 -39.
                        if (currentDigit == '3') outputNumeral += "und" + numeral + "ßig";
                        // 40 - 99					
                        if (currentDigit >= (int)'4') outputNumeral += "und" + numeral + "zig";
                        break;
                    case 2:
                        // 100 - 999
                        outputNumeral = numeral + "hundert" + outputNumeral;
                        break;
                }
            }

            // Add thousands & millions.
            if (lengthInputFloat > 3)
            {
                counterThousands++;
                outputNumeral = (counterThousands == 1) ?
                                "tausend" + outputNumeral : "millionen" + outputNumeral;

                outputNumeral = convertFloatToNumeral(inputFloat.Substring
                                (0, inputFloat.Length - 3), "", counterThousands) + outputNumeral;
            }

            // Build outputNumeral decimal part.
            string floatDecimals = floatSplittedAtDecimals[1];

            if (hasDecimals && outputNumeral.IndexOf("komma") == -1)
            {
                outputNumeral += "komma";
                for (int a = 0; a < floatDecimals.Length; a++)
                {
                    if (floatDecimals[a] == '0') outputNumeral += "null";
                    outputNumeral += numeralEquivOfDigit[(char)floatDecimals[a]];
                }
            }

            // Correct naming conventions.
            outputNumeral = outputNumeral.Replace("siebenzig", "siebzig");
            outputNumeral = (outputNumeral.EndsWith("ein")) ?
                outputNumeral.Substring(0, outputNumeral.Length - 3) + "eins" : outputNumeral;
            outputNumeral = (outputNumeral.IndexOf("einsmillionen") >= -1) ?
                outputNumeral.Replace("einsmillionen", "einemillion") : outputNumeral;

            return outputNumeral;
        }


        // 10.
        public static string convertNumeralToRationalNum(string numeral = "")
        {
            Dictionary<string, int> digitEquivOfNumeral = new Dictionary<string, int>
        {
            {"null", 0 },
            { "und", 1},
            { "ein", 1 },
            { "zwei", 2 },
            { "drei", 3 },
            { "vier", 4 },
            { "fuenf", 5 },
            { "sech", 6 },
            { "sechs", 6 },
            { "sieben", 7 },
            { "sieb", 7 },
            { "acht", 8 },
            { "neun", 9 },
            { "zehn", 10 },
            { "zig", 10 },
            { "elf", 11 },
            { "zwoelf", 12 },
            { "hundert", 100 },
            { "tausend", 1000 },
            { "million", 1000000 },
            { "millionen", 1000000 }
        };
            numeral = numeral.Replace("ßig", "zig").Replace("eine", "ein")
                             .Replace("eins", "ein").Replace("zwan", "zwei")
                             .Replace("halbe", "zwei").Replace("drit", "drei")
                             .Replace("zwan", "zwei").Replace("halbe", "zwei")
                             .Replace("drit", "drei");

            // Prepare for fraction.
            int indexSpace = numeral.IndexOf(' ');
            bool isFraction = (indexSpace == -1) ? false : true;
            string denominator = (isFraction) ? numeral.Substring(indexSpace + 1)
                                                       .Replace("stel", "").Replace("tel", "")
                                                       .ToLower() : "";
            numeral = (isFraction) ? numeral.Replace(" ", "").Substring(0, indexSpace) : numeral;

            // Prepare for decimals.
            int indexKomma = numeral.IndexOf("komma");
            bool hasDecimals = (indexKomma == -1) ? false : true;
            string decimalsNumeral = (hasDecimals) ? "," + numeral.Substring(indexKomma + 5) : "";
            numeral = (hasDecimals) ? numeral.Replace("komma", "").Substring(0, indexKomma) : numeral;
            if (hasDecimals)
            {
                foreach (KeyValuePair<string, int> equiv in digitEquivOfNumeral)
                {
                    if (equiv.Key == "sieb" || equiv.Key == "sech") continue;
                    decimalsNumeral = decimalsNumeral.Replace(equiv.Key, equiv.Value.ToString());
                }
            }

            // Build number from numeral.
            int finalNum = 0;
            int tempNum = 0;
            int lastVal = 0;
            int product = 0;
            int cnt = 0;
            bool greaterThousand = false;
            bool hasMillions = false;

            while (numeral != "")
            {
                foreach (KeyValuePair<string, int> equiv in digitEquivOfNumeral)
                {
                    int val = equiv.Value;
                    string key = equiv.Key;

                    if (numeral.EndsWith(key))
                    {
                        int pos = numeral.LastIndexOf(key);
                        numeral = numeral.Remove(pos, key.Length);

                        // Exception 1 - 19.
                        if (val <= 12 && key != "zig" && cnt == 0)
                        {
                            tempNum += val;
                            lastVal = 1;
                            cnt--;
                            break;
                        }
                        // Exception 1.000 - 19.999
                        if (val <= 12 && key != "zig" && greaterThousand)
                        {
                            greaterThousand = false;
                            tempNum += val;
                            lastVal = 1;
                            break;
                        }
                        greaterThousand = false;

                        // Concat numbers.
                        if (cnt % 2 == 1) product = val * lastVal;
                        if (hasMillions) product = val * lastVal / 1000;

                        if (val < 1000)
                        {
                            tempNum += product;
                        }
                        else if (val == 1000000)
                        {
                            hasMillions = true;
                        }
                        else
                        {
                            finalNum = tempNum;
                            tempNum = 0;
                            lastVal = 1;
                            cnt++;
                            greaterThousand = true;
                            break;
                        }
                        lastVal = val;
                        product = 0;
                        break;
                    }

                    // Avoid infinite loop.
                    if (numeral != "" && cnt > 31)
                        return numeral + " Error: Unknown rational number numeral, exit program.";
                }
                cnt++;
            }
            finalNum = (finalNum == 0) ? tempNum : finalNum + tempNum * 1000;
            if (finalNum >= 10000000)
                return "Error: Input limited to 10.000.000";

            return (isFraction) ?
                finalNum.ToString() + "/" + convertNumeralToRationalNum(denominator) :
                finalNum.ToString() + decimalsNumeral;
        }


        // 10. Alternative Logic.
        /*
		if (numeral.IndexOf("million") >= 0) lengthNumeral = 7;
		if (numeral.IndexOf("zehnmillionen") >= 0) lengthNumeral = 8;
		if ((numeral.IndexOf("hundert") >= 0 && numeral.IndexOf("tausend") >= 0) && 
			(numeral.IndexOf("hundert") < numeral.IndexOf("tausend"))) lengthNumeral = 6;
		if (numeral.IndexOf("tausend") >= 0 && lengthNumeral == 0) lengthNumeral = 5;			
		if (numeral.IndexOf("tausend") >= 0 && lengthNumeral == 0) lengthNumeral = 4; // geht nicht mehr nach 5.
		if (numeral.IndexOf("hundert") >= 0 && numeral.IndexOf("tausend") == -1) lengthNumeral = 3;
		if (numeral.IndexOf("hundert") == -1 && numeral.IndexOf("tausend") == -1 && 
			!numeralEquivOfDigit.ContainsValue(numeral)) lengthNumeral = 2;
		if (numeral != "" && lengthNumeral == 0) lengthNumeral = 1; // think about that :)	
		*/


        // 11.
        public static void printPascalsTriangle(int depth)
        {
            // Define h <= 15.
            depth = (depth > 15) ? 15 : depth;

            int[] baseTriangle = new int[depth];
            int[] tempBaseTriangle = new int[depth];
            baseTriangle[0] = 1;
            baseTriangle[1] = 1;
            string spacer = "\u0020";

            for (int a = 0; a < depth; a++)
            {
                // Calculate triangle base line.			
                for (int b = 0; b <= a; b++)
                {
                    tempBaseTriangle[0] = 1;
                    if (b > 0)
                    {
                        tempBaseTriangle[b] = baseTriangle[b - 1] + baseTriangle[b];
                    }
                }
                // !!! arrays by reference, simple data types by value!
                baseTriangle = (int[])tempBaseTriangle.Clone();

                // Draw spacers.
                for (int b = 0; b < depth - a; b++)
                {
                    Console.Write(spacer);
                }

                // Draw triangle base line.
                for (int b = 0; b < depth; b++)
                {
                    if (baseTriangle[b] == 0)
                    {
                        continue;
                    }
                    else
                    {
                        Console.Write(baseTriangle[b] + spacer);
                    }
                }
                Console.WriteLine();
            }
        }


        // 11. php equivalent for functionality testing.
        /*
        $depth = 5;
        $baseTriangle = array_fill(0, $depth, 0);
        $tempBaseTriangle = array_fill(0, $depth, 0);
        $baseTriangle[0] = 1;
        $baseTriangle[1] = 1;
        $spacer = ' ';

        for ($a = 0; $a < $depth; $a++)
        {	
            // calculate triangle base line			
            for ($b = 0; $b <= $a; $b++)
            {
                $tempBaseTriangle[0] = 1;
                if ($b > 0) {
                    $tempBaseTriangle[$b] = $baseTriangle[$b - 1] + $baseTriangle[$b];
                }
            }
            $baseTriangle = $tempBaseTriangle;

            // draw spacers..
            for ($b = 0; $b < $depth - $a; $b++)
            {
                echo $spacer;		
            }

            // ..and triangle base line
            for ($b = 0; $b < $depth; $b++)
            {
                if ($baseTriangle[$b] == 0) {
                    continue; 
                } else {
                    echo $baseTriangle[$b] . $spacer;
                }
            }
            echo PHP_EOL;
        }		
        */


        // 12.
        public static void printChristmasTree(int h)
        {
            string spacer = "\u0020";
            string branch = "\u0023";
            string trunk = "\u007C";

            int countLevels = h;

            while (countLevels != 1)
            {
                // Draw spacers.
                for (int a = 0; a < countLevels; a++)
                {
                    Console.Write(spacer);
                }

                // Draw branches.
                Console.Write(branch);
                for (int a = 0; a < h - countLevels; a++)
                {
                    Console.Write(branch + branch);
                }
                Console.WriteLine();

                countLevels--;
            }

            // Draw trunk.
            for (int a = 0; a < h - 1; a++)
            {
                Console.Write(spacer);
            }
            Console.Write(trunk + trunk);
        }
    }
}