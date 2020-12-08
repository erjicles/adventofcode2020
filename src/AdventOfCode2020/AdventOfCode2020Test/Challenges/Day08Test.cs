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
        public void TryRunProgramTest()
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
                BootCodeHelper.TryRunProgram(bootCode, out int actual);
                Assert.Equal(testExample.Item2, actual);
            }
        }

        [Fact]
        public void TryFindFixedBootCodeTest()
        {
            // For example, consider the same program from above:
            // nop + 0
            // acc + 1
            // jmp + 4
            // acc + 3
            // jmp - 3
            // acc - 99
            // acc + 1
            // jmp - 4
            // acc + 6
            // If you change the first instruction from nop +0 to jmp +0, it 
            // would create a single - instruction infinite loop, never leaving 
            // that instruction.If you change almost any of the jmp 
            // instructions, the program will still eventually find another 
            // jmp instruction and loop forever.
            // However, if you change the second - to - last instruction
            // (from jmp -4 to nop - 4), the program terminates! The 
            // instructions are visited in this order:
            // nop + 0 | 1
            // acc + 1 | 2
            // jmp + 4 | 3
            // acc + 3 |
            // jmp - 3 |
            // acc - 99 |
            // acc + 1 | 4
            // nop - 4 | 5
            // acc + 6 | 6
            // After the last instruction(acc +6), the program terminates by 
            // attempting to run the instruction below the last instruction in 
            // the file. With this change, after the program terminates, the 
            // accumulator contains the value 8(acc + 1, acc + 1, acc + 6).
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
                    }, 8)
            };

            foreach (var testExample in testData)
            {
                var bootCode = BootCodeHelper.ParseInputLines(testExample.Item1);
                var foundSuccessfulProgram = BootCodeHelper.TryFindFixedProgram(bootCode, out _, out int actual);
                Assert.True(foundSuccessfulProgram);
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

        [Fact]
        public void GetDay08Part02AnswerTest()
        {
            int expected = 2251;
            int actual = Day08.GetDay08Part02Answer();
            Assert.Equal(expected, actual);
        }
    }
}
