private string _ship_via_code;
public string ship_via_code
{
    get
    {
        return _ship_via_code;
    }
    set
    {
        //check if value is null or empty
        if (String.IsNullOrEmpty(value))
        {
            throw new Exception("Ship Via Code is required.");
        }
        //check if value is less than or equal to 4 characters after trimm
        else if (value.Trim().Length > 4)
        {
            throw new Exception("Ship Via Code cannot be greater than 4 characters.");
        }
        else
        {
            _ship_via_code = value.Trim();
        }
    }
}
