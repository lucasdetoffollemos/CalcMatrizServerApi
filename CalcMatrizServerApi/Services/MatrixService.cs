using CalcMatrizServerApi.Models;

namespace CalcMatrizServerApi.Services
{
    public class MatrixService
    {
        public MatrixReturn MultiplyMatrixes(double[][] matrixA, double[][] matrixB, int index, int len)
        {
            var matrixAConverted = ConvertTo2DArray(matrixA);

            var matrixBConverted = ConvertTo2DArray(matrixB);

            int rows = matrixAConverted.GetLength(0);
            int cols = matrixBConverted.GetLength(1);


            // Create the result matrix with appropriate dimensions (rowsA x colsB)
            double[,] resultMatrix = new double[rows, cols];
            // Perform matrix multiplication
            for (int i = 0; i < rows; i++)      // Loop through rows of matrix A
            {
                for (int j = 0; j < cols; j++)   // Loop through columns of matrix B
                {
                    double result = 0;
                    for (int k = 0; k < cols; k++)   // Loop through columns of matrix A / rows of matrix B
                    {
                        result += matrixAConverted[i, k] * matrixBConverted[k, j];
                    }
                    resultMatrix[i, j] = Math.Round(result, 4);
                }
            }
            //Array.Copy(resultMatrix, 0, matrixResult, index, len);

            return new MatrixReturn { matrixResult = ConvertToJaggedArray(resultMatrix) };
        }

        public static double[,] ConvertTo2DArray(double[][] jaggedArray)
        {
            // Validate input
            if (jaggedArray == null || jaggedArray.Length == 0)
                throw new ArgumentException("Jagged array cannot be null or empty.");

            // Get dimensions for the 2D array
            int rows = jaggedArray.Length;
            int cols = jaggedArray[0].Length; // Assumes all rows in jagged array are the same length

            // Create the 2D array
            double[,] result = new double[rows, cols];

            // Fill the 2D array
            for (int i = 0; i < rows; i++)
            {
                if (jaggedArray[i].Length != cols)
                    throw new ArgumentException("All rows in the jagged array must have the same length.");

                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = jaggedArray[i][j];
                }
            }

            return result;
        }

        public static double[][] ConvertToJaggedArray(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            double[][] jaggedArray = new double[rows][];

            for (int i = 0; i < rows; i++)
            {
                jaggedArray[i] = new double[cols];
                for (int j = 0; j < cols; j++)
                {
                    jaggedArray[i][j] = matrix[i, j];
                }
            }

            return jaggedArray;
        }
    }
}
