namespace Hy.Helper;

public static class RandomHelper
{
    /// <summary>
    /// 根据权重随机获取index
    /// </summary>
    /// <param name="itemsWithWeight">项值为权重</param>
    /// <returns>数组的index</returns>
    public static int Random(float[] itemsWithWeight)
    {
        var random = new Random(~unchecked((int)DateTime.Now.Ticks));
        var randomNumber = random.Next(1, 101);
        var each = 100f / itemsWithWeight.Sum();
        for (var i = 0; i < itemsWithWeight.Length; i++)
        {
            if (randomNumber <= itemsWithWeight.Take(i + 1).Sum() * each)
            {
                return i;
            }
        }

        return itemsWithWeight.Length - 1;
    }
}