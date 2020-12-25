using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day25
{
    public static class DoorHelper
    {
        public static BigInteger GetEncryptionKey(
            BigInteger publicKeyDoor, 
            BigInteger publicKeyCard)
        {
            var loopSizeDoor = GetLoopSizeFromPublicKey(publicKeyDoor);
            Console.WriteLine($"Door loop size: {loopSizeDoor}");
            var loopSizeCard = GetLoopSizeFromPublicKey(publicKeyCard);
            Console.WriteLine($"Card loop size: {loopSizeCard}");

            var encryptionKeyDoor = GetTransformedSubjectNumber(loopSizeDoor, publicKeyCard);
            Console.WriteLine($"Encryption key (door): {encryptionKeyDoor}");
            var encryptionKeyCard = GetTransformedSubjectNumber(loopSizeCard, publicKeyDoor);
            Console.WriteLine($"Encryption key (card): {encryptionKeyCard}");

            if (encryptionKeyCard != encryptionKeyDoor)
            {
                throw new Exception($"Encryption keys don't match: card: {encryptionKeyCard}, door: {encryptionKeyDoor}");
            }

            return encryptionKeyCard;
        }

        public static int GetLoopSizeFromPublicKey(BigInteger publicKey)
        {
            int result = 1;
            BigInteger currentTransform = 1;
            while (true)
            {
                currentTransform = (currentTransform * 7) % 20201227;
                if (currentTransform == publicKey)
                {
                    break;
                }
                result++;
            }
            return result;
        }

        public static BigInteger GetTransformedSubjectNumber(int loopSize, BigInteger subjectNumber)
        {
            BigInteger result = 1;
            for (int i = 0; i < loopSize; i++)
            {
                result = (result * subjectNumber) % 20201227;
            }
            return result;
        }
    }
}
