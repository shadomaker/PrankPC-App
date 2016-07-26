using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using System.Windows.Forms;
using System.Media;


//
//Application Name - DrunkPC
//Description - Application that generates erratic inputs
//Topics - (1) Threads 
//         (2) Systems.Windows.Forms Namespace
//         (3) Hidden GUI
//
//Program Credits: Shado (Programmer /Icon Designer/ Compiler) | Barnacules Nerdgasm (MSDN Source Code References/ INT32 support)
//© All rights reserved 2016
//
//Twitter @shadomaker
//soundcloud: wwww.soundcloud.com/shadomaker
//

namespace DrunkPC
{
    class Program
    {
        public static Random _random = new Random();

        public static int _StartupDelaySeconds = 10;
        public static int _TotalDurationSeconds = 10;

        /// <summary>
        /// Entry Point for Application
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Drunk PC Prank Application Created By Shado :P");

            if (args.Length >= 2)
            {
                _StartupDelaySeconds = Convert.ToInt32(args[0]);
                _TotalDurationSeconds = Convert.ToInt32(args[1]);
            }

            //create all threads for Prank Application to make it multithreaded
            Thread drunkMouseThread = new Thread(new ThreadStart(DrunkMouseThread));
            Thread drunkKeyboardThread = new Thread(new ThreadStart(DrunkKeyboardThread));
            Thread drunkSoundThread = new Thread(new ThreadStart(DrunkSoundThread));
            Thread drunkPopupThread = new Thread(new ThreadStart(DrunkPopupThread));

            Console.WriteLine("Waiting 10 Seconds then starting threads");
            DateTime future = DateTime.Now.AddSeconds(_StartupDelaySeconds);
            while (future > DateTime.Now)
            {
                Thread.Sleep(1000);
            }

            //start all threads
            drunkMouseThread.Start();
            drunkKeyboardThread.Start();
            drunkSoundThread.Start();
            drunkPopupThread.Start();

            //wait for user input
            Console.Read();

            future = DateTime.Now.AddSeconds(_TotalDurationSeconds);
            while (future > DateTime.Now)
            {
                Thread.Sleep(1000);
            }

            Console.WriteLine("Ending Threads Now");

            //kill all threads and wait for user input
            drunkMouseThread.Abort();
            drunkKeyboardThread.Abort();
            drunkSoundThread.Abort();
            drunkPopupThread.Abort();

        }
        #region Thread Functions
        /// <summary>
        /// Mouse Movement Generator
        /// </summary>
        public static void DrunkMouseThread()
        {

            Console.WriteLine("DrunkMouseThread Started");

            int moveX = 0;
            int moveY = 0;

            while (true)
            {
                //Console.WriteLine(Cursor.Position.ToString());

                //random number gen to affect mouse cursor pos
                moveX = _random.Next(20) - 10;
                moveY = _random.Next(20) - 10;

                //change mouse pos to new random co-ordinates
                Cursor.Position = new System.Drawing.Point(
                    Cursor.Position.X +moveX, 
                    Cursor.Position.Y +moveY);

                Thread.Sleep(50);
            }
        }

        /// <summary>
        /// Keyboard Input Generator
        /// </summary>
        public static void DrunkKeyboardThread()
        {

            Console.WriteLine("DrunkKeyboardThread Started");

            while (true)
            {
                //generate random keys
                char key = (char)(_random.Next(25)+65);

                //50/50 make it lowercase
                if (_random.Next(2) == 0)
                {
                    key = Char.ToLower(key);
                }

                SendKeys.SendWait(key.ToString());

                Thread.Sleep(_random.Next(1000));
            }
        }

        /// <summary>
        /// Generates Error Sounds Randomly
        /// </summary>
        public static void DrunkSoundThread()
        {

            Console.WriteLine("DrunkSoundThread Started");

            while (true)
            {
                if (_random.Next(100) > 90)
                {
                    {
                        switch (_random.Next(5))
                        {
                            case 0:
                                SystemSounds.Asterisk.Play();
                                break;
                            case 1:
                                SystemSounds.Beep.Play();
                                break;
                            case 2:
                                SystemSounds.Exclamation.Play();
                                break;
                            case 3:
                                SystemSounds.Hand.Play();
                                break;
                            case 4:
                                SystemSounds.Question.Play();
                                break;

                        }
                    }

                }

                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// Dispays fake error messages to end user
        /// </summary>
        public static void DrunkPopupThread()
        {

            Console.WriteLine("DrunkPopupThread Started");

            while (true)
            {
                //Every 10 Seconds roll dice
                if (_random.Next(100) > 90)
                {
                    switch (_random.Next(2))
                    {
                        case 0:
                            MessageBox.Show(
                            "Internet Explorer has stopped working",
                            "Internet Explorer",
                            MessageBoxButtons.OK
                            , MessageBoxIcon.Error);
                            break;

                        case 1:
                            MessageBox.Show(
                            "Your system is running low on resources",
                            "Microsoft Windows",
                            MessageBoxButtons.OK
                            , MessageBoxIcon.Warning);
                            break;
                    }
                }
                Thread.Sleep(10000);
            }
        }
    }
}
#endregion