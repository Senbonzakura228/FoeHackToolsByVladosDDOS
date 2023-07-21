public class MainDataStorage
{
    public static AccountParams AccountParams;
    public static RequestService RequestService;
    public static Ages UserAge = Ages.IndustrialAge;

    public static AgeCounterPicker GetUserAgeCounterPicker()
    {
        return MainDataStorage.UserAge switch
        {
            Ages.IndustrialAge => IndustrialAgePicker.GetInstance(),
            Ages.ProgressiveEra => ProgressiveEraCounterPicker.GetInstance(),
            Ages.SpaceAgeMars => SpaceAgeMarsPicker.GetInstance(),
            _ => ProgressiveEraCounterPicker.GetInstance(),
        };
    }
}