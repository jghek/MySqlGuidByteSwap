namespace MySqlGuidByteSwap.Tests;

using FluentAssertions;

public class MySqlGuidTest
{
	internal class MySqlGuidResult
	{
		public byte[] bin { get; set; }
		public string uuid { get; set; }
	}

	//[Fact]
	//public void Parse_GivenByte16_ReturnsCsCompliantGuid()
	//{
	//	const string query = @"SELECT 0x00112233445566778899AABBCCDDEEFF bin, BIN_TO_UUID(0x00112233445566778899AABBCCDDEEFF) uuid;";
	//	const string expectedString = "00112233-4455-6677-8899-AABBCCDDEEFF";

	//	var expected = Guid.Parse(expectedString);

	//	var result = Connection.QuerySingle<MySqlGuidResult>(query);

	//	expected.Should().Be(MySqlGuid.Parse(result.bin));
	//}

	[Fact]
	public void Parse_GivenInvalidBytes_ThrowsException()
	{
		Action act = () => MySqlGuid.Parse([]);

		act.Should().Throw<ArgumentOutOfRangeException>();
	}

	//[Fact]
	//public void ToByteArray_GivenGuid_ReturnsMySqlCompliantBin()
	//{
	//	const string query = @"SELECT 0x00112233445566778899AABBCCDDEEFF bin, BIN_TO_UUID(0x00112233445566778899AABBCCDDEEFF) uuid;";
	//	const string expectedString = "00112233-4455-6677-8899-AABBCCDDEEFF";

	//	var expected = Guid.Parse(expectedString);
	//	var result = Connection.QuerySingle<MySqlGuidResult>(query);

	//	var resultGuid = MySqlGuid.Parse(result.bin);
	//	var resultBin = MySqlGuid.ToByteArray(resultGuid);

	//	resultBin.Should().BeEquivalentTo(result.bin);
	//}

	[Fact]
	public void SwapMySqlBytes_GivenGuid_ReturnsMySqlCompliantGuid()
	{
		const string givenString = "00112233-4455-6677-8899-AABBCCDDEEFF";
		const string expectedString = "33221100-5544-7766-8899-aabbccddeeff";

		var given = Guid.Parse(givenString);
		var expected = Guid.Parse(expectedString);
		var result = given.SwapMySqlBytes();

		result.Should().Be(expected);
		result.SwapMySqlBytes().Should().Be(given);
	}
}