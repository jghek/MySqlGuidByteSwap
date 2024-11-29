# MySqlGuidByteSwap

MySqlGuidByteSwap is a utility package that simplifies handling GUIDs (Globally Unique Identifiers) when working with MySQL databases. MySQL stores GUIDs in a binary format with a different byte order than .NET/C#. This library provides methods to convert GUIDs to and from MySQL's binary representation, making it easy to store and retrieve GUIDs in their correct format.

![Icon](https://raw.githubusercontent.com/jghek/MySqlGuidByteSwap/refs/heads/main/assets/icon.png)  
*Icon designed by [FlatIcon.com](https://www.flaticon.com).*

---

## Features

- **Convert GUIDs to MySQL's binary format:** Easily prepare GUIDs for storage in MySQL.
- **Parse GUIDs from MySQL binary representation:** Read GUIDs from MySQL and convert them back to their C# representation.
- **Extension method for GUID swapping:** Perform the transformation directly on a GUID using an extension method.

---

## Installation

Install via NuGet Package Manager:

```bash
Install-Package MySqlGuidByteSwap
```

Or use the .NET CLI:

```bash
dotnet add package MySqlGuidByteSwap
```

---

## Usage

### Convert GUID to MySQL Binary Format

```csharp
using MySqlGuidByteSwap;

Guid originalGuid = Guid.NewGuid();
byte[] mysqlBytes = MySqlGuid.ToByteArray(originalGuid);

Console.WriteLine($"Original GUID: {originalGuid}");
Console.WriteLine($"MySQL Binary Format: {BitConverter.ToString(mysqlBytes)}");
```

### Parse GUID from MySQL Binary Format

```csharp
using MySqlGuidByteSwap;

byte[] mysqlBytes = { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF };
Guid parsedGuid = MySqlGuid.Parse(mysqlBytes);

Console.WriteLine($"Parsed GUID: {parsedGuid}");
```

### Swap GUID Bytes for MySQL (Extension Method)

```csharp
using MySqlGuidByteSwap;

Guid originalGuid = Guid.NewGuid();
Guid swappedGuid = originalGuid.SwapMySqlBytes();

Console.WriteLine($"Original GUID: {originalGuid}");
Console.WriteLine($"Swapped GUID: {swappedGuid}");
```

---

## API Reference

### Methods

#### `MySqlGuid.ToByteArray(Guid guid)`
Converts a .NET `Guid` to its MySQL binary representation.

- **Parameters:** 
  - `guid` (Guid): The GUID to transform.
- **Returns:** A `byte[]` containing the transformed MySQL binary representation.

#### `MySqlGuid.Parse(byte[] b)`
Parses a MySQL binary representation of a GUID into a .NET `Guid`.

- **Parameters:** 
  - `b` (byte[]): A 16-byte array representing a GUID in MySQL format.
- **Returns:** A `Guid` parsed from the byte array.

#### `Guid.SwapMySqlBytes(this Guid guid)`
An extension method to swap a `Guid`'s bytes for MySQL storage or retrieval.

- **Parameters:** 
  - `guid` (Guid): The GUID to transform.
- **Returns:** A new `Guid` with the swapped byte order.

---

## Example MySQL Usage

When storing GUIDs in a MySQL database, you may use `BINARY(16)` as the column type. Use this package to ensure that GUIDs are converted correctly for storage and retrieval.

### Insert a GUID into MySQL

```csharp
using MySqlGuidByteSwap;
using MySql.Data.MySqlClient;

// Assuming a MySQL connection
Guid guid = Guid.NewGuid();
byte[] mysqlBytes = MySqlGuid.ToByteArray(guid);

using (var connection = new MySqlConnection("your_connection_string"))
{
    connection.Open();
    var command = new MySqlCommand("INSERT INTO your_table (id) VALUES (@id)", connection);
    command.Parameters.AddWithValue("@id", mysqlBytes);
    command.ExecuteNonQuery();
}
```

### Retrieve a GUID from MySQL

```csharp
using MySqlGuidByteSwap;
using MySql.Data.MySqlClient;

using (var connection = new MySqlConnection("your_connection_string"))
{
    connection.Open();
    var command = new MySqlCommand("SELECT id FROM your_table WHERE ...", connection);
    using (var reader = command.ExecuteReader())
    {
        if (reader.Read())
        {
            byte[] mysqlBytes = (byte[])reader["id"];
            Guid guid = MySqlGuid.Parse(mysqlBytes);
            Console.WriteLine($"Retrieved GUID: {guid}");
        }
    }
}
```

---

## License

This package is licensed under the MIT License. See the `LICENSE` file for more details.

--- 

For more information, bug reports, or feature requests, visit the [GitHub Repository](https://github.com/your-repo/mysql-guid-byteswap).
