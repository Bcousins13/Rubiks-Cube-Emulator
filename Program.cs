using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace RubiksCube
{
    class Program
    {
        static void Main()
        {
            //Build the solved cube and colour dictionary
            List<Cubie> All_cube = new List<Cubie>();
            Dictionary<int, string> ColourDict = new Dictionary<int, string>();
            MakeColourDict(ColourDict);
            All_cube = GenerateCubies(All_cube);
            All_cube = CubeColourReset(All_cube);

            //Print instructions and intro to user
            Console.WriteLine("Welcome the the rubiks cube emulator!");
            Console.WriteLine("Your lists of commands are as follows:");
            Console.WriteLine("FRONT Clockwise = F; FRONT Anti-Clockwise = F'");
            Console.WriteLine("BACK Clockwise = B; BACK Anti-Clockwise = B'");
            Console.WriteLine("LEFT Clockwise = L; LEFT Anti-Clockwise = L'");
            Console.WriteLine("RIGHT Clockwise = R; RIGHT Anti-Clockwise = R'");
            Console.WriteLine("UP Clockwise = U; UP Anti-Clockwise = U'");
            Console.WriteLine("DOWN Clockwise = D; DOWN Anti-Clockwise = D'");
            Console.WriteLine("The cube will show after each command input ");

            //Show the solved cube 
            PrintTheCube(All_cube, ColourDict);

            //initialise the "game"
            for (int i = 0; i < 10000; i++)
            {
                DoTheCommand(All_cube, ColourDict);
                i++;
            }

        }
        /// <summary>
        /// Takes users command input, does the correct rotation and prints the cube
        /// </summary>
        /// <param name="All_cube"></param> 
        /// <param name="ColourDict"></param>
        public static void DoTheCommand(List<Cubie> All_cube, Dictionary<int,string> ColourDict)
        {
            string command = "";
            Console.WriteLine("Please enter a command:");
            command = Console.ReadLine();

            if (command == "F" || command == "f")
            {
                RotateFrontBackFace90(All_cube, 1, 1);
            }
            else if (command == "F'" || command == "f'")
            {
                RotateFrontBackFace90(All_cube, 1, -1);
            }
            else if (command == "B" || command == "b")
            {
                RotateFrontBackFace90(All_cube, -1, 1);
            }
            else if (command == "B'" || command == "b'")
            {
                RotateFrontBackFace90(All_cube, -1, -1);
            }
            else if (command == "L" || command == "l")
            {
                RotateLeftRightFace90(All_cube, -1, 1);
            }
            else if (command == "L'" || command == "l'")
            {
                RotateLeftRightFace90(All_cube, -1, -1);
            }
            else if (command == "R" || command == "r")
            {
                RotateLeftRightFace90(All_cube, 1, 1);
            }
            else if (command == "R'" || command == "r'")
            {
                RotateLeftRightFace90(All_cube, 1, -1);
            }
            else if (command == "U" || command == "u")
            {
                RotateBottomTopFace90(All_cube, 1, 1);
            }
            else if (command == "U'" || command == "u'")
            {
                RotateBottomTopFace90(All_cube, 1, -1);
            }
            else if (command == "D" || command == "d")
            {
                RotateBottomTopFace90(All_cube, -1, 1);
            }
            else if (command == "D'" || command == "d'")
            {
                RotateBottomTopFace90(All_cube, -1, -1);
            }

            PrintTheCube(All_cube, ColourDict);
        }

        /// <summary>
        /// Takes the colours from each of the cubies and prints them in the correct position 
        /// </summary>
        /// <param name="All_cube"></param>
        /// <param name="ColourDict"></param>
        public static void PrintTheCube(List<Cubie> All_cube, Dictionary<int, string> ColourDict)
        {
            //use the ColourFind Method to get a 3x3 array of the colours on each face
            string[,] Left_Face = ColourFind(All_cube, ColourDict, -1);
            string[,] Right_Face = ColourFind(All_cube, ColourDict, 1);
            string[,] Front_Face = ColourFind(All_cube, ColourDict, 3);
            string[,] Back_Face = ColourFind(All_cube, ColourDict, -3);
            string[,] Top_Face = ColourFind(All_cube, ColourDict, 2);
            string[,] Bottom_Face = ColourFind(All_cube, ColourDict, -2);

            //write the colours to the console for the user using the matrix array elements of each face
            Console.WriteLine("                      ------------------------                            ");
            Console.WriteLine("                      " + "||" + Top_Face[0, 0] + ":" + Top_Face[0, 1] + ":" + Top_Face[0, 2] + "||" + "                           ");
            Console.WriteLine("                      " + "||" + Top_Face[1, 0] + ":" + Top_Face[1, 1] + ":" + Top_Face[1, 2] + "||" + "                           ");
            Console.WriteLine("                      " + "||" + Top_Face[2, 0] + ":" + Top_Face[2, 1] + ":" + Top_Face[2, 2] + "||" + "                           ");
            Console.WriteLine("--------------------------------------------------------------------------------------------");
            Console.WriteLine("||" + Left_Face[0, 0] + ":" + Left_Face[0, 1] + ":" + Left_Face[0, 2] + "||" + Front_Face[0, 0] + ":" + Front_Face[0, 1] + ":" + Front_Face[0, 2] + "||" + Right_Face[0, 0] + ":" + Right_Face[0, 1] + ":" + Right_Face[0, 2] + "||" + Back_Face[0, 0] + ":" + Back_Face[0, 1] + ":" + Back_Face[0, 2] + "||");
            Console.WriteLine("||" + Left_Face[1, 0] + ":" + Left_Face[1, 1] + ":" + Left_Face[1, 2] + "||" + Front_Face[1, 0] + ":" + Front_Face[1, 1] + ":" + Front_Face[1, 2] + "||" + Right_Face[1, 0] + ":" + Right_Face[1, 1] + ":" + Right_Face[1, 2] + "||" + Back_Face[1, 0] + ":" + Back_Face[1, 1] + ":" + Back_Face[1, 2] + "||");
            Console.WriteLine("||" + Left_Face[2, 0] + ":" + Left_Face[2, 1] + ":" + Left_Face[2, 2] + "||" + Front_Face[2, 0] + ":" + Front_Face[2, 1] + ":" + Front_Face[2, 2] + "||" + Right_Face[2, 0] + ":" + Right_Face[2, 1] + ":" + Right_Face[2, 2] + "||" + Back_Face[2, 0] + ":" + Back_Face[2, 1] + ":" + Back_Face[2, 2] + "||");
            Console.WriteLine("--------------------------------------------------------------------------------------------");
            Console.WriteLine("                      " + "||" + Bottom_Face[0, 0] + ":" + Bottom_Face[0, 1] + ":" + Bottom_Face[0, 2] + "||" + "                           ");
            Console.WriteLine("                      " + "||" + Bottom_Face[1, 0] + ":" + Bottom_Face[1, 1] + ":" + Bottom_Face[1, 2] + "||" + "                           ");
            Console.WriteLine("                      " + "||" + Bottom_Face[2, 0] + ":" + Bottom_Face[2, 1] + ":" + Bottom_Face[2, 2] + "||" + "                           ");
            Console.WriteLine("                      ------------------------                            ");

        }

        /// <summary>
        /// Rotates an NxN matrics 90 degress anti-clockwise
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="n"></param>
        public static string[,] RotateACW90(string[,] arr, int n)
        {
            string[,] temp = new string[n,n];
            //this loop moves the elements of the array that was input to the rotated position 
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    temp[i, j] = arr[j, n-i-1];
                }
            }
            return temp;
        }

        /// <summary>
        /// Rotates an NxN matrix 90 degress clockwise
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="n"></param>
        public static string[,] RotateCW90(string[,] arr, int n)
        {
            string[,] temp = new string[n, n];
            //this loop moves the elements of the array that was input to the rotated position 
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    temp[i, j] = arr[n - j - 1, i];
                }
            }
            return temp;
        }

        /// <summary>
        /// Flips an NxN matrix along its diagonal from [0,0] to [n,n]
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string[,] FlipMatrix(string[,]arr, int n)
        {
            string[,] temp = new string[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    temp[i, j] = arr[j, i];
                }
            }
            return temp;
        }

        /// <summary>
        /// Finds the colour of a face based in in put parameters
        /// </summary>
        /// <returns> </returns>
        public static string[,] ColourFind(List<Cubie> cube, Dictionary<int, string> dict, int facefinder)
        {
            string[,] face = new string[3, 3];

            if (Math.Abs(facefinder) == 1)
            {
                face = LEFTRIGHTFaceFinder(cube, dict, facefinder);
            }
            else if (Math.Abs(facefinder) == 2)
            {
                face = TOPBOTTOMFaceFinder(cube, dict, facefinder);
            }
            else if (Math.Abs(facefinder) == 3)
            {
                face = FRONTBACKFaceFinder(cube, dict, facefinder);
            }
            return face;
           
        }

        /// <summary>
        /// Finds all cubies on either right or left face and adds their colours to a 3x3 string array
        /// </summary>
        public static string[,] LEFTRIGHTFaceFinder(List<Cubie> cube, Dictionary<int, string> dict, int facefinder)
        {
            string[,] face = new string[3, 3];
            //iterate through whole cube 
            foreach (var item in cube)
            {
                //if their x position is 1 or -1 then they are on the left or right hand side of the cube
                if (item.position.xpos == facefinder)
                {
                    //use the colour attribute of the cubie to find its equivalent colour string in the dictionary 
                    string thecolour = dict[item.colour.xcol];
                    //add this string to the array using the positional values of the cubie
                    //+1 to postions as they can be -1 and so they fit into the simension of the array
                    face[item.position.ypos + 1, item.position.zpos + 1] = thecolour;
                }
            }
            //rotations of array based on where the face is on the cube 
            if (facefinder > 0)
            {
                face = RotateCW90(face, 3);
                face = RotateCW90(face, 3);
            }
            if (facefinder < 0)
            {
                face = RotateCW90(face, 3);
                face = FlipMatrix(face, 3);
            }
            return face;
        }

        /// <summary>
        /// Finds all cubies on either top or bottom face and adds their colours to a 3x3 string array
        /// </summary>
        public static string[,] TOPBOTTOMFaceFinder(List<Cubie> cube, Dictionary<int, string> dict, int facefinder)
        {
            //set the facefinder to either 1 or -1 to be used in the foreach loop below as positions are unit vectors
            if (facefinder < 0)
            {
                facefinder = (facefinder / facefinder) * -1;
            }
            else
            {
                facefinder = facefinder / facefinder;
            }
            string[,] face = new string[3, 3];
            //iterate through whole cube 
            foreach (var item in cube)
            {
                //if their y position is 1 or -1 then they are on the top or bottom of the cube

                if (item.position.ypos == facefinder)
                {                    
                    //use the colour attribute of the cubie to find its equivalent colour string in the dictionary 
                    string thecolour = dict[item.colour.ycol];
                    //add this string to the array using the positional values of the cubie
                    //+1 to postions as they can be -1 and so they fit into the simension of the array
                    face[item.position.xpos + 1, item.position.zpos + 1] = thecolour;
                }
            }
            //rotations of array based on where the face is on the cube 
            if (facefinder > 0) 
            {
                face = FlipMatrix(face, 3);
            }
            if (facefinder < 0)
            {
                face = RotateACW90(face, 3);
            }
            

            return face;
        }

        /// <summary>
        /// Finds all cubies on either front or back face and adds their colours to a 3x3 string array
        /// </summary>
        public static string[,] FRONTBACKFaceFinder(List<Cubie> cube, Dictionary<int, string> dict, int facefinder)
        {
            //set the facefinder to either 1 or -1 to be used in the foreach loop below as positions are unit vectors
            if (facefinder < 0)
            {
                facefinder = (facefinder / facefinder) * -1;
            }
            else
            {
                facefinder = facefinder / facefinder;
            }
            string[,] face = new string[3, 3];
            //iterate through whole cube 
            foreach (var item in cube)
            {
                //if their z position is 1 or -1 then they are on the front or back of the cube
                if (item.position.zpos == facefinder)
                {
                    //use the colour attribute of the cubie to find its equivalent colour string in the dictionary 
                    string thecolour = dict[item.colour.zcol];
                    //add this string to the array using the positional values of the cubie
                    //+1 to postions as they can be -1 and so they fit into the simension of the array
                    face[item.position.xpos + 1, item.position.ypos + 1] = thecolour;
                }
                
            }
            //rotations of array based on where the face is on the cube 
            if (facefinder > 0 )
            {
                face = RotateACW90(face, 3);
            }
            if (facefinder < 0)
            {
                face = RotateACW90(face, 3);
                face = RotateACW90(face, 3);
                face = FlipMatrix(face, 3);
            }
            

            return face;
        }

        /// <summary>
        /// Generates the colour dictionary for the Rubiks cube 
        /// COLOURS: 4 = green, 5 = blue, 6 = red, 7 = orange, 8 = white, 9 = yellow
        /// </summary>
        public static Dictionary<int, string> MakeColourDict(Dictionary<int, string> dict)
        {
            dict.Add(4, "GREEN ");
            dict.Add(5, " BLUE ");
            dict.Add(6, " RED  ");
            dict.Add(7, "ORANGE");
            dict.Add(8, " WHITE");
            dict.Add(9, "YELLOW");

            return dict;
        }

        /// <summary>
        /// Generates a list of cubies with unit vector positions 
        /// </summary>
        public static List<Cubie> GenerateCubies(List<Cubie> cube)
        {
            
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    for (int k = -1; k < 2; k++)
                    {
                        //if (i == 0 && j == 0 && k == 0)
                        //{
                        //    break;
                        //}
                        Cubie_Pos TestPosition = new Cubie_Pos();
                        Cubie_Colour Test_colour = new Cubie_Colour();
                        Cubie testcubie = new Cubie(TestPosition, Test_colour);
                        TestPosition.xpos = i;
                        TestPosition.ypos = j;
                        TestPosition.zpos = k;
                        testcubie.position = TestPosition;

                       cube.Add(testcubie);
                    }
                }
            }
            return cube;
        }
        
        /// <summary>
        /// Generates a solved rubiks cube by setting the colour attribute of each cubie in the list
        /// </summary>
        /// 
        public static List<Cubie> CubeColourReset(List<Cubie> cube)
        {
            foreach (var item in cube)
            {
                if (item.position.zpos == 1)
                {
                    item.colour.zcol = 4;
                }
                if (item.position.zpos == -1)
                {
                    item.colour.zcol = 5;
                }
                if (item.position.xpos == 1)
                {
                    item.colour.xcol = 6;
                }
                if (item.position.xpos == -1)
                {
                    item.colour.xcol = 7;
                }
                if (item.position.ypos == 1)
                {
                    item.colour.ycol = 8;
                }
                if (item.position.ypos == -1)
                {
                    item.colour.ycol = 9;
                }
            }
            return cube;
        }

        /// <summary>
        /// Rotate the front or back face of the cube by 90 degrees 
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="g"></param>
        /// <returns></returns>
        public static List<Cubie> RotateFrontBackFace90(List<Cubie> cube, int g, int CWorACW)
        {
            //this ensures the method works for clockwise and anti clockwise rotation for both faces 
            //as their directions are mirrored 
            CWorACW = CWorACW * g;
            //iterate through whole cube 
            foreach (var item in cube)
            {
                //check if front or back face
                //front: z = 1 back: z = -1
                if (item.position.zpos == g)
                {
                    int tempcol = 0;
                    int temppos = 0;
                    //vector 90 degree rotation set x = y and y = -x 
                    //change values based on face and direction
                    //anticlockwise rotation
                    if (CWorACW < 0)
                    {
                        temppos = item.position.xpos;
                        item.position.xpos = -item.position.ypos;
                        item.position.ypos = temppos;
                    }
                    //clockwise rotation
                    if (CWorACW > 0)
                    {
                        temppos = item.position.ypos;
                        item.position.ypos = -item.position.xpos;
                        item.position.xpos = temppos;
                    }
                    //rotate the colours on the cubies based on same logic as position
                    //for side pieces one of the colours will be 0 so need to check for that
                    if (item.colour.ycol == 0)
                    {
                        item.colour.ycol = item.colour.xcol;
                        item.colour.xcol = 0;
                    }
                    else if (item.colour.xcol == 0)
                    {
                        item.colour.xcol = item.colour.ycol;
                        item.colour.ycol = 0;
                    }
                    else
                    {
                        tempcol = item.colour.ycol;
                        item.colour.ycol = item.colour.xcol;
                        item.colour.xcol = tempcol;
                    }
                }
            }
            return cube;

        }

        /// <summary>
        /// Rotate the left or right face of the cube by 90 degrees
        /// </summary>
        public static List<Cubie> RotateLeftRightFace90(List<Cubie> cube, int h, int CWorACW)
        {
            //this ensures the method works for clockwise and anti clockwise rotation for both faces 
            //as their directions are mirrored 
            CWorACW = CWorACW * h;
            //iterate through whole cube 
            foreach (var item in cube)
            {
                //check if left or right face
                //front: x = 1 back: x = -1
                if (item.position.xpos == h)
                {
                    int tempcol = 0;
                    int temppos = 0;
                    //vector 90 degree rotation set x = y and y = -x 
                    //change values based on face and direction
                    //anticlockwise rotation
                    if (CWorACW < 0)
                    {
                        temppos = item.position.ypos;
                        item.position.ypos = -item.position.zpos;
                        item.position.zpos = temppos;
                    }
                    //clockwise rotation
                    if (CWorACW > 0)
                    {
                        temppos = item.position.zpos;
                        item.position.zpos = -item.position.ypos;
                        item.position.ypos = temppos;
                    }
                    //rotate the colours on the cubies based on same logic as position
                    //for side pieces one of the colours will be 0 so need to check for that
                    if (item.colour.ycol == 0)
                    {
                        item.colour.ycol = item.colour.zcol;
                        item.colour.zcol = 0;
                    }
                    else if (item.colour.zcol == 0)
                    {
                        item.colour.zcol = item.colour.ycol;
                        item.colour.ycol = 0;
                    }
                    else
                    {
                        tempcol = item.colour.zcol;
                        item.colour.zcol = item.colour.ycol;
                        item.colour.ycol = tempcol;
                    }
                }
            }
            return cube;
        }

        /// <summary>
        /// Rotate the top or bottom face of the cube by 90 degrees
        /// </summary>
        public static List<Cubie> RotateBottomTopFace90(List<Cubie> cube, int j, int CWorACW)
        {
            //this ensures the method works for clockwise and anti clockwise rotation for both faces 
            //as their directions are mirrored 
            CWorACW = CWorACW * j;
            //iterate through whole cube 
            foreach (var item in cube)
            {
                //check if top or bottom face
                //front: y = 1 back: y = -1
                if (item.position.ypos == j)
                {
                    int tempcol = 0;
                    int temppos = 0;
                    //vector 90 degree rotation set x = y and y = -x 
                    //change values based on face and direction
                    //clockwise rotation
                    if (CWorACW > 0)
                    {
                        temppos = item.position.xpos;
                        item.position.xpos = -item.position.zpos;
                        item.position.zpos = temppos;
                    }
                    //anticlockwise rotation
                    if (CWorACW < 0)
                    {
                        temppos = item.position.zpos;
                        item.position.zpos = -item.position.xpos;
                        item.position.xpos = temppos;
                    }
                    //rotate the colours on the cubies based on same logic as position
                    //for side pieces one of the colours will be 0 so need to check for that
                    if (item.colour.xcol == 0)
                    {
                        item.colour.xcol = item.colour.zcol;
                        item.colour.zcol = 0;
                    }
                    else if (item.position.zpos == 0)
                    {
                        item.colour.zcol = item.colour.xcol;
                        item.colour.xcol = 0;
                    }
                    else
                    {
                        tempcol = item.colour.zcol;
                        item.colour.zcol = item.colour.xcol;
                        item.colour.xcol = tempcol;
                    }
                }
            }
            return cube;
        }
        
    }

    /// <summary>
    /// Attribue of cubie which contains the colours of each of the faces of the cube 
    /// </summary>
    class Cubie_Colour
    {
        public int xcol, ycol, zcol;
    }

    /// <summary>
    /// Attribue of cubie which contains the positions of the cube
    /// </summary>
    class Cubie_Pos
    {
        public int xpos, ypos, zpos; 
    }

    /// <summary>
    /// the class tha contains the information for a cubie: Colours and Position
    /// </summary>
    class Cubie
    {
        public Cubie_Colour colour;
        public Cubie_Pos position; 

        public Cubie( Cubie_Pos pos, Cubie_Colour col)
        {
            position = pos; 
            colour = col;
        }
    }
}
