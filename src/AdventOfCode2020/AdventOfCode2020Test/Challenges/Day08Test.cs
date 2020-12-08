using AdventOfCode2020.Challenges.Day08;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2020Test.Challenges
{
    public class Day08Test
    {
        [Fact]
        public void RunProgramTest()
        {
            // For example, consider the following program:
            // nop +0
            // acc +1
            // jmp +4
            // acc +3
            // jmp -3
            // acc -99
            // acc +1
            // jmp -4
            // acc +6
            // These instructions are visited in this order:
            // nop +0 | 1
            // acc +1 | 2, 8(!)
            // jmp +4 | 3
            // acc +3 | 6
            // jmp -3 | 7
            // acc -99 |
            // acc +1 | 4
            // jmp -4 | 5
            // acc +6 |
            // First, the nop +0 does nothing. Then, the accumulator is 
            // increased from 0 to 1(acc + 1) and jmp +4 sets the next 
            // instruction to the other acc +1 near the bottom.After it 
            // increases the accumulator from 1 to 2, jmp - 4 executes, 
            // setting the next instruction to the only acc +3.It sets the 
            // accumulator to 5, and jmp -3 causes the program to continue 
            // back at the first acc + 1.
            // This is an infinite loop: with this sequence of jumps, the 
            // program will run forever.The moment the program tries to run 
            // any instruction a second time, you know it will never terminate.
            //Immediately before the program would run an instruction a 
            // second time, the value in the accumulator is 5.
            var testData = new List<Tuple<IList<string>, int>>()
            {
                new Tuple<IList<string>, int>(
                    new List<string>()
                    {
                        "nop +0",
                        "acc +1",
                        "jmp +4",
                        "acc +3",
                        "jmp -3",
                        "acc -99",
                        "acc +1",
                        "jmp -4",
                        "acc +6"
                    }, 5)
            };

            foreach (var testExample in testData)
            {
                var bootCode = BootCodeHelper.ParseInputLines(testExample.Item1);
                var actual = BootCodeHelper.RunProgram(bootCode);
                Assert.Equal(testExample.Item2, actual);
            }
        }

        [Fact]
        public void GetDay08Part01AnswerTest()
        {
            int expected = 2014;
            int actual = Day08.GetDay08Part01Answer();
            Assert.Equal(expected, actual);
        }
    }
}
