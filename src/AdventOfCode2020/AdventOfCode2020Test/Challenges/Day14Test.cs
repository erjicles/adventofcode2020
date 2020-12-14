using AdventOfCode2020.Challenges.Day14;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2020Test.Challenges
{
    public class Day14Test
    {
        [Fact]
        public void GetValueWithMaskAppliedTest()
        {
            // The program then attempts to write the value 11 to memory address 8.By expanding everything out to individual bits, the mask is applied as follows:
            //
            // value: 000000000000000000000000000000001011(decimal 11)
            // mask: XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
            // result: 000000000000000000000000000001001001(decimal 73)
            // So, because of the mask, the value 73 is written to memory address 8 instead.Then, the program tries to write 101 to address 7:
            //
            // value: 000000000000000000000000000001100101(decimal 101)
            // mask: XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
            // result: 000000000000000000000000000001100101(decimal 101)
            // This time, the mask has no effect, as the bits it overwrote were already the values the mask tried to set. Finally, the program tries to write 0 to address 8:
            //
            // value: 000000000000000000000000000000000000(decimal 0)
            // mask: XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
            // result: 000000000000000000000000000001000000(decimal 64)
            var testData = new List<Tuple<string, long, long>>()
            {
                new Tuple<string, long, long>(
                    "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X",
                    11,
                    73),
                new Tuple<string, long, long>(
                    "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X",
                    101,
                    101),
                new Tuple<string, long, long>(
                    "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X",
                    0,
                    64)
            };

            foreach (var testExample in testData)
            {
                var actual = DockingProgramHelper.GetValueWithMaskApplied(testExample.Item2, testExample.Item1);
                Assert.Equal(testExample.Item3, actual);
            }
        }

        [Fact]
        public void GetAddressesWithMaskAppliedTest()
        {
            // For example, consider the following program:
            // mask = 000000000000000000000000000000X1001X
            // mem[42] = 100
            // mask = 00000000000000000000000000000000X0XX
            // mem[26] = 1
            // When this program goes to write to memory address 42, it first 
            // applies the bitmask:
            // address: 000000000000000000000000000000101010(decimal 42)
            // mask: 000000000000000000000000000000X1001X
            // result:  000000000000000000000000000000X1101X
            // After applying the mask, four bits are overwritten, three of 
            // which are different, and two of which are floating. Floating 
            // bits take on every possible combination of values; with two 
            // floating bits, four actual memory addresses are written:
            // 000000000000000000000000000000011010(decimal 26)
            // 000000000000000000000000000000011011(decimal 27)
            // 000000000000000000000000000000111010(decimal 58)
            // 000000000000000000000000000000111011(decimal 59)
            // Next, the program is about to write to memory address 26 with a 
            // different bitmask:
            // address: 000000000000000000000000000000011010(decimal 26)
            // mask: 00000000000000000000000000000000X0XX
            // result:  00000000000000000000000000000001X0XX
            // This results in an address with three floating bits, causing 
            // writes to eight memory addresses:
            // 000000000000000000000000000000010000(decimal 16)
            // 000000000000000000000000000000010001(decimal 17)
            // 000000000000000000000000000000010010(decimal 18)
            // 000000000000000000000000000000010011(decimal 19)
            // 000000000000000000000000000000011000(decimal 24)
            // 000000000000000000000000000000011001(decimal 25)
            // 000000000000000000000000000000011010(decimal 26)
            // 000000000000000000000000000000011011(decimal 27)
            var testData = new List<Tuple<string, long, IList<long>>>()
            {
                new Tuple<string, long, IList<long>>(
                    "000000000000000000000000000000X1001X",
                    42,
                    new List<long>() { 26, 27, 58, 59 }),
                new Tuple<string, long, IList<long>>(
                    "00000000000000000000000000000000X0XX",
                    26,
                    new List<long>() { 16, 17, 18, 19, 24, 25, 26, 27 })
            };

            foreach (var testExample in testData)
            {
                var actual = DockingProgramHelper.GetAddressesWithMaskApplied(testExample.Item2, testExample.Item1);
                bool contentsAreTheSame = actual.Count == testExample.Item3.Count
                    && !actual.Where(e => !testExample.Item3.Contains(e)).Any();
                Assert.True(contentsAreTheSame);
            }
        }

        [Fact]
        public void GetSumOfMemoryValuesTest()
        {
            // For example, consider the following program:
            // mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
            // mem[8] = 11
            // mem[7] = 101
            // mem[8] = 0
            // This program starts by specifying a bitmask(mask = ....).
            // The mask it specifies will overwrite two bits in every written 
            // value: the 2s bit is overwritten with 0, and the 64s bit is 
            // overwritten with 1.
            // The program then attempts to write the value 11 to memory 
            // address 8. By expanding everything out to individual bits, the 
            // mask is applied as follows:
            // value: 000000000000000000000000000000001011(decimal 11)
            // mask: XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
            // result: 000000000000000000000000000001001001(decimal 73)
            // So, because of the mask, the value 73 is written to memory 
            // address 8 instead.Then, the program tries to write 101 to 
            // address 7:
            // value: 000000000000000000000000000001100101(decimal 101)
            // mask: XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
            // result: 000000000000000000000000000001100101(decimal 101)
            // This time, the mask has no effect, as the bits it overwrote 
            // were already the values the mask tried to set. Finally, the 
            // program tries to write 0 to address 8:
            // value: 000000000000000000000000000000000000(decimal 0)
            // mask: XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
            // result: 000000000000000000000000000001000000(decimal 64)
            // 64 is written to address 8 instead, overwriting the value that 
            // was there previously.
            // To initialize your ferry's docking program, you need the sum of 
            // all values left in memory after the initialization program 
            // completes. (The entire 36-bit address space begins initialized 
            // to the value 0 at every address.) In the above example, only 
            // two values in memory are not zero - 101 (at address 7) and 64 
            // (at address 8) - producing a sum of 165.
            //
            // Version 2
            // For example, consider the following program:
            // mask = 000000000000000000000000000000X1001X
            // mem[42] = 100
            // mask = 00000000000000000000000000000000X0XX
            // mem[26] = 1
            // When this program goes to write to memory address 42, it first 
            // applies the bitmask:
            // address: 000000000000000000000000000000101010(decimal 42)
            // mask: 000000000000000000000000000000X1001X
            // result:  000000000000000000000000000000X1101X
            // After applying the mask, four bits are overwritten, three of 
            // which are different, and two of which are floating. Floating 
            // bits take on every possible combination of values; with two 
            // floating bits, four actual memory addresses are written:
            // 000000000000000000000000000000011010(decimal 26)
            // 000000000000000000000000000000011011(decimal 27)
            // 000000000000000000000000000000111010(decimal 58)
            // 000000000000000000000000000000111011(decimal 59)
            // Next, the program is about to write to memory address 26 with a 
            // different bitmask:
            // address: 000000000000000000000000000000011010(decimal 26)
            // mask: 00000000000000000000000000000000X0XX
            // result:  00000000000000000000000000000001X0XX
            // This results in an address with three floating bits, causing 
            // writes to eight memory addresses:
            // 000000000000000000000000000000010000(decimal 16)
            // 000000000000000000000000000000010001(decimal 17)
            // 000000000000000000000000000000010010(decimal 18)
            // 000000000000000000000000000000010011(decimal 19)
            // 000000000000000000000000000000011000(decimal 24)
            // 000000000000000000000000000000011001(decimal 25)
            // 000000000000000000000000000000011010(decimal 26)
            // 000000000000000000000000000000011011(decimal 27)
            // The entire 36 - bit address space still begins initialized to 
            // the value 0 at every address, and you still need the sum of all 
            // values left in memory at the end of the program.
            // In this example, the sum is 208.
            var testData = new List<Tuple<IList<string>, int, long>>()
            {
                new Tuple<IList<string>, int, long>(
                    new List<string>()
                    {
                        "mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X",
                        "mem[8] = 11",
                        "mem[7] = 101",
                        "mem[8] = 0"
                    }, 1, 165),
                new Tuple<IList<string>, int, long>(
                    new List<string>()
                    {
                        "mask = 000000000000000000000000000000X1001X",
                        "mem[42] = 100",
                        "mask = 00000000000000000000000000000000X0XX",
                        "mem[26] = 1"
                    }, 2, 208),
            };

            foreach (var testExample in testData)
            {
                var memoryBank = DockingProgramHelper.RunProgram(testExample.Item1, testExample.Item2);
                var actual = DockingProgramHelper.GetSumOfMemoryValues(memoryBank);
                Assert.Equal(testExample.Item3, actual);
            }
        }
        
        [Fact]
        public void GetDay14Part01AnswerTest()
        {
            long expected = 6513443633260;
            long actual = Day14.GetDay14Part01Answer();
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void GetDay14Part02AnswerTest()
        {
            long expected = 3442819875191;
            long actual = Day14.GetDay14Part02Answer();
            Assert.Equal(expected, actual);
        }
    }
}
