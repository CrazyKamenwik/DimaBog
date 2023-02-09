namespace SkyDrive
{
    /// <summary>
    /// My own mapper. It's not working with collections. This file going to be deleted in the next PR. You may not check it.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TK"></typeparam>
    public class MyMapper<T, TK>
        where T : new()
        where TK : new()
    {
        public TK Map(T firstModel)
        {
            var secondModel = new TK();

            return Map(firstModel, secondModel);
        }

        public TK Map(T firstModel, TK secondModel)
        {
            if (secondModel is null)
            {
                return Map(firstModel);
            }

            var firstAttributes = typeof(T).GetProperties();
            var secondAttributes = typeof(TK).GetProperties();

            foreach (var firstAttribute in firstAttributes)
            {
                foreach (var secondAttribute in secondAttributes)
                {
                    if (firstAttribute.Name != secondAttribute.Name) continue;

                    var value = firstAttribute.GetValue(firstModel);
                    secondAttribute.SetValue(secondModel, value);

                    break;
                }
            }

            return secondModel;
        }
    }
}
