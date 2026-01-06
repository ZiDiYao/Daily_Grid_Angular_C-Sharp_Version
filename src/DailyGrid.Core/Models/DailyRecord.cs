namespace DailyGrid.Core.Models;

public class DailyRecord
{

    public DateTime Timestamp { get; }

    public int ScreenActiveSecondsDelta { get; private set; }
    public int MouseClicksDelta { get; private set; }
    public int KeyPressesDelta { get; private set; }

    // builder
    public DailyRecord(DateTime dateTime, int ScreenActiveSecondsDelta, int MouseClicksDelta, int KeyPressesDelta)
    {
        Timestamp = timestamp;
        SetDeltas(screenActiveSecondsDelta, mouseClicksDelta, keyPressesDelta);

    }

    public void SetDeltas()
    {

        if (screenActiveSecondsDelta < 0) throw new ArgumentOutOfRangeException(nameof(screenActiveSecondsDelta));
        if (mouseClicksDelta < 0) throw new ArgumentOutOfRangeException(nameof(mouseClicksDelta));
        if (keyPressesDelta < 0) throw new ArgumentOutOfRangeException(nameof(keyPressesDelta));

        ScreenActiveSecondsDelta = screenActiveSecondsDelta;
        MouseClicksDelta = mouseClicksDelta;
        KeyPressesDelta = keyPressesDelta;


    }

    public override string ToString()
        => $"{Timestamp:o} | screen+{ScreenActiveSecondsDelta} | mouse+{MouseClicksDelta} | key+{KeyPressesDelta}";
    
 
 }