using System.Security.Cryptography;
using System.Text;

namespace TradeWind.Shared.Helpers;

public static class PasswordHelper
{
	private const int SaltSize = 16; // 128 bit
	private const int HashSize = 32; // 256 bit
	private const int Iterations = 10000;


	/// <summary>
	/// Generates a secure hash for the specified password using PBKDF2 with a randomly generated salt.
	/// </summary>
	/// <param name="password">The plain text password to be hashed.</param>
	/// <returns>A Base64-encoded string containing the salt and hash.</returns>
	/// <exception cref="ArgumentException">Thrown when the provided password is null or empty.</exception>
	public static string HashPassword(string password)
	{
		if (string.IsNullOrEmpty(password))
		{
			throw new ArgumentException("Password cannot be null or empty.", nameof(password));
		}

		byte[] salt = GenerateSalt();
		byte[] hash = HashPasswordWithSalt(password, salt);

		// Combine salt and hash into a single byte array
		byte[] saltAndHash = new byte[salt.Length + hash.Length];
		Buffer.BlockCopy(salt, 0, saltAndHash, 0, salt.Length);
		Buffer.BlockCopy(hash, 0, saltAndHash, salt.Length, hash.Length);

		return Convert.ToBase64String(saltAndHash);
	}

	/// <summary>
	/// Generates a cryptographically secure random salt for use in password hashing.
	/// </summary>
	/// <returns>A byte array containing the generated salt.</returns>
	private static byte[] GenerateSalt()
	{
		byte[] salt = new byte[SaltSize];
		using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
		{
			rng.GetBytes(salt);
		}

		return salt;
	}

	/// <summary>
	/// Computes a hash of the specified password using PBKDF2 with the given salt.
	/// </summary>
	/// <param name="password">The plain text password to be hashed.</param>
	/// <param name="salt">The cryptographic salt to use for hashing.</param>
	/// <returns>A byte array containing the derived hash.</returns>
	private static byte[] HashPasswordWithSalt(string password, byte[] salt)
	{
		using (var pbkdf2 = new Rfc2898DeriveBytes(
				password: Encoding.UTF8.GetBytes(password),
				salt: salt,
				iterations: Iterations,
				hashAlgorithm: HashAlgorithmName.SHA256))
		{
			return pbkdf2.GetBytes(HashSize);
		}
	}

	/// <summary>
	/// Verifies whether the provided plain text password matches the specified hashed password.
	/// This method extracts the salt from the stored hash, re-computes the hash using the same salt and parameters,
	/// and performs a constant-time comparison to prevent timing attacks.
	/// </summary>
	/// <param name="password">The plain text password to verify.</param>
	/// <param name="hashedPassword">The Base64-encoded string containing the salt and hash to compare against.</param>
	/// <returns>True if the password is valid and matches the hash; otherwise, false.</returns>
	public static bool VerifyPassword(string password, string hashedPassword)
	{
		if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hashedPassword))
			return false;

		try
		{
			byte[] saltAndHash = Convert.FromBase64String(hashedPassword);

			if (saltAndHash.Length != SaltSize + HashSize)
				return false;

			byte[] salt = new byte[SaltSize];
			byte[] hash = new byte[HashSize];

			Array.Copy(saltAndHash, 0, salt, 0, SaltSize);
			Array.Copy(saltAndHash, SaltSize, hash, 0, HashSize);

			byte[] newHash = HashPasswordWithSalt(password, salt);

			return SlowEquals(hash, newHash);
		}
		catch
		{
			return false;
		}
	}

	/// <summary>
	/// Compares two byte arrays in a constant-time manner to mitigate timing attacks.
	/// </summary>
	/// <param name="a">The first byte array to compare.</param>
	/// <param name="b">The second byte array to compare.</param>
	/// <returns>True if both arrays are equal; otherwise, false.</returns>
	private static bool SlowEquals(byte[] a, byte[] b)
	{
		if (a.Length != b.Length)
			return false;

		uint diff = 0;
		for (int i = 0; i < a.Length; i++)
		{
			diff |= (uint)(a[i] ^ b[i]);
		}
		return diff == 0;
	}

}
