# C# Coding Standards and Naming Conventions


| Object Name               | Notation   | Length | Plural | Prefix | Suffix | Abbreviation | Char Mask          | Underscores |
|:--------------------------|:-----------|-------:|:-------|:-------|:-------|:-------------|:-------------------|:------------|
| Class name                | PascalCase |    128 | No     | No     | Yes    | No           | [A-z][0-9]         | No          |
| Constructor name          | PascalCase |    128 | No     | No     | Yes    | No           | [A-z][0-9]         | No          |
| Method name               | PascalCase |    128 | Yes    | No     | No     | No           | [A-z][0-9]         | No          |
| Method arguments          | camelCase  |    128 | Yes    | No     | No     | Yes          | [A-z][0-9]         | No          |
| Local variables           | camelCase  |     50 | Yes    | No     | No     | Yes          | [A-z][0-9]         | No          |
| Constants name            | PascalCase |     50 | No     | No     | No     | No           | [A-z][0-9]         | No          |
| Field name                | camelCase  |     50 | Yes    | No     | No     | Yes          | [A-z][0-9]         | Yes         |
| Properties name           | PascalCase |     50 | Yes    | No     | No     | Yes          | [A-z][0-9]         | No          |
| Delegate name             | PascalCase |    128 | No     | No     | Yes    | Yes          | [A-z]              | No          |
| Enum type name            | PascalCase |    128 | Yes    | No     | No     | No           | [A-z]              | No          |

#### 1. Sử dụng PascalCasing cho tên lớp và tên phương thức:

```csharp
public class ClientActivity
{
  public void ClearStatistics()
  {
    //...
  }
  public void CalculateStatistics()
  {
    //...
  }
}
```

***Why: consistent with the Microsoft's .NET Framework and easy to read.***

#### 2.Sử dụng camelCasing cho các đối số phương thức và biến cục bộ:

```csharp
public class UserLog
{
  public void Add(LogEvent logEvent)
  {
    int itemCount = logEvent.Items.Count;
    // ...
  }
}
```

***Why: consistent with the Microsoft's .NET Framework and easy to read.***

#### 3. Do not use Hungarian notation or any other type identification in identifiers

```csharp
// Correct
int counter;
string name;    
// Avoid
int iCounter;
string strName;
```

***Why: consistent with the Microsoft's .NET Framework and Visual Studio IDE makes determining types very easy (via tooltips). In general you want to avoid type indicators in any identifier.***

#### 4. Không sử dụng Screaming Caps cho các hằng số hoặc các biến chỉ đọc:

```csharp
// Correct
public const string ShippingType = "DropShip";
// Avoid
public const string SHIPPINGTYPE = "DropShip";
```


#### 5. Tránh sử dụng Viết tắt. Ngoại lệ: các chữ viết tắt thường được sử dụng làm tên, chẳng hạn như Id, Xml, Ftp, Uri.

```csharp    
// Correct
UserGroup userGroup;
Assignment employeeAssignment;     
// Avoid
UserGroup usrGrp;
Assignment empAssignment; 
// Exceptions
CustomerId customerId;
XmlDocument xmlDocument;
FtpHelper ftpHelper;
UriPart uriPart;
```

***Why: phù hợp với .NET Framework của Microsoft và ngăn các chữ viết tắt không nhất quán.***



#### 6. Không sử dụng Dấu gạch dưới trong số nhận dạng. Ngoại lệ: bạn có thể đặt trước các trường riêng tư bằng dấu gạch dưới:

```csharp 
// Correct
public DateTime clientAppointment;
public TimeSpan timeLeft;    
// Avoid
public DateTime client_Appointment;
public TimeSpan time_Left; 
// Exception (Class field)
private DateTime _registrationDate;
```

***Why: phù hợp với .NET Framework của Microsoft và làm cho mã tự nhiên hơn để đọc . Đồng thời tránh nhấn mạnh gạch chân (không thể nhìn thấy gạch chân.***

#### 7. Sử dụng các tên kiểu được xác định trước  như `int`,` float`, `string` cho các khai báo cục bộ, tham số và thành viên. Sử dụng các tên .NET Framework như `Int32`,` Single`, `String` khi truy cập các thành viên tĩnh của loại như` Int32.TryParse` hoặc `String.Join`.

```csharp
// Correct
string firstName;
int lastIndex;
bool isSaved;
string commaSeparatedNames = String.Join(", ", names);
int index = Int32.Parse(input);
// Avoid
String firstName;
Int32 lastIndex;
Boolean isSaved;
string commaSeparatedNames = string.Join(", ", names);
int index = int.Parse(input);
```

***Why: phù hợp với .NET Framework của Microsoft và làm cho mã tự nhiên hơn để đọc.*** 



#### 11. Sử dụng danh từ hoặc cụm danh từ để đặt tên cho một lớp.

```csharp 
public class Employee
{
}
public class BusinessLocation
{
}
public class DocumentCollection
{
}
```

***Why: phù hợp với .NET Framework của Microsoft và dễ nhớ***

#### 12. Làm giao diện tiền tố với chữ cái I. Tên giao diện là danh từ (cụm từ) hoặc tính từ.

```csharp     
public interface IShape
{
}
public interface IShapeCollection
{
}
public interface IGroupable
{
}
```

#### 13. Đặt tên tệp nguồn theo các lớp chính của chúng. Ngoại lệ: tên tệp có các lớp một phần phản ánh nguồn hoặc mục đích của chúng, ví dụ: nhà thiết kế, tạo, v.v.. 

```csharp 
// Located in Task.cs
public partial class Task
{
}
// Located in Task.generated.cs
public partial class Task
{
}
```



#### 17.Sử dụng tên số ít cho enums. Ngoại lệ: trường bit enums.

```csharp 
// Correct
public enum Color
{
  Red,
  Green,
  Blue,
  Yellow,
  Magenta,
  Cyan
} 
// Exception
[Flags]
public enum Dockings
{
  None = 0,
  Top = 1,
  Right = 2, 
  Bottom = 4,
  Left = 8
}
```

#### 18. Không chỉ định rõ ràng loại enum hoặc các giá trị của enum (ngoại trừ trường bit):

```csharp 
// Don't
public enum Direction : long
{
  North = 1,
  East = 2,
  South = 3,
  West = 4
} 
// Correct
public enum Direction
{
  North,
  East,
  South,
  West
}
```

#### 19. Không sử dụng hậu tố "Enum" trong tên loại enum:

```csharp     
// Don't
public enum CoinEnum
{
  Penny,
  Nickel,
  Dime,
  Quarter,
  Dollar
} 
// Correct
public enum Coin
{
  Penny,
  Nickel,
  Dime,
  Quarter,
  Dollar
}
```



