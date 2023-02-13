# ATHERCRC32
ATHERCRC32 Provide developers with a complete framework for using CRC32 in functions/classes in memory, and protecting your software against WriteProcessMemory or changes during execution in memory, protecting your intellectual property. But in C#

Based of https://github.com/keowu/ATHERCRC32 

Check https://github.com/keowu/ATHERCRC32 to see what it does and how it works.

# Example
```csharp
            Action myFunction = () => Console.WriteLine("Hello World");

            AntiMemoryWriteDetection.DetectMemoryWrite(myFunction, true);
```
