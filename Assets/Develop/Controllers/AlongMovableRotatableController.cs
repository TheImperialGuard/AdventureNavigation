public class AlongMovableRotatableController : Controller
{
    private IMovable _movable;

    private IDirectionRotatable _rotatable;

    public AlongMovableRotatableController(IMovable movable, IDirectionRotatable rotatable)
    {
        _movable = movable;
        _rotatable = rotatable;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        _rotatable.SetRotateDirection(_movable.CurrentVelocity);
    }
}
