namespace AtherCRC32
{
    public static class CRC32
    {
        private static readonly uint[] crcTable = new uint[]
        {
            0x00000000, 0x77073096, 0xEE0E612C, 0x990951BA, 0x076DC419,
            0x706AF48F, 0xE963A535, 0x9E6495A3, 0x0EDB8832, 0x79DCB8A4,
            0xE0D5E91E, 0x97D2D988, 0x09B64C2B, 0x7EB17CBD, 0xE7B82D07,
            0x90BF1D91, 0x1DB71064, 0x6AB020F2, 0xF3B97148, 0x84BE41DE,
        };

        public static uint CalculateCRC32ForFunction(Delegate function)
        {
            byte[] functionBytes = GetFunctionBytes(function);
            return CalculateCRC32(functionBytes);
        }

        public static byte[] GetFunctionBytes(Delegate function)
        {
            MethodInfo methodInfo = function.Method;
            MethodBody methodBody = methodInfo.GetMethodBody();
            byte[] functionBytes = methodBody.GetILAsByteArray();
            return functionBytes;
        }

        public static uint CalculateCRC32(byte[] functionBytes)
        {
            uint crc = 0xFFFFFFFF;

            foreach (byte b in functionBytes)
            {
                crc = (crc >> 8) ^ crcTable[(crc ^ b) & 0xFF];
            }

            crc = crc ^ 0xFFFFFFFF;
            return crc;
        }
    }

    public class AntiMemoryWriteDetection
    {
        public static void DetectMemoryWrite(Delegate function, bool showWarning)
        {
            uint originalCRC32 = CRC32.CalculateCRC32ForFunction(function);
            uint currentCRC32 = CRC32.CalculateCRC32ForFunction(function);

            if (originalCRC32 != currentCRC32)
            {
                Console.WriteLine("Detected change in memory block of function.");

                if (showWarning)
                {
                    Console.WriteLine("Please ensure that your intellectual property is protected.");
                }
            }
            else
            {
                Console.WriteLine("No change detected in memory block of function.");
            }
        }
      }
}
