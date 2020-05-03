using UniRx;

public static class Timers
{
    public static void AddTimer(GeometryObjectModel fastenedFigure, float stepTime)
    {
        Observable.Timer(System.TimeSpan.FromSeconds(stepTime)).Repeat().Subscribe(_ =>
        {
            fastenedFigure.SetRandomColor();

        }).AddTo(fastenedFigure);
    }
}