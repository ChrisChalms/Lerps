# Lerps
A tiny lerping library encapsulates Unity's existing lerping/slerping, and adds a few QoL features such as pausing, resuming, updating, grouping, and scheduling.

# Usage

To use a Lerp, you'll need to first create the lerp, group, or sequence you want to use then step it
```c#
void Update() => _alphaLerp?.Step();
```

## Creating Lerps
```c#
// The simplest inline lerp
Lerper.Lerp(start, end, durationInMs);

// Using some handy, optional callbacks and custom easing types
Lerper.Lerp(start, end, durationInMs, easeType, UpdatePosition, FinishedMoving);
// or
Lerper.Lerp(start, end, durationInMs, onUpdate: UpdatePosition);

// Cache the lerp which stops existing lerp if present
_alphaLerp = Lerper.Lerp(start, end, durationInMs, onUpdate: newAlpha => _canvasGroup.alpha = newAlpha);

// Create a lerp from cached/shared settings
Lerper.Lerp(sharedSettings);

```

## Functionality
```c#
// Any lerp can be reset, paused, resumed, or updated
_cachedLerp = Lerper.Lerp(startAlpha, endAlpha, fadeTime, onUpdate: newAlpha => _canvasGroup.alpha = newAlpha);
...
_cachedLerp.Suspend();
_cachedLerp.Resume();
_cachedLerp.Reset();
_cachedLerp.UpdateDuration(newTimeInMs);
```

## Groups
You can group multiple lerps together into interact with them as one 
```c#
_moveGroup = new LerpGroup(GetRandomRotationLerp(), GetFadeLerp());
_moveGroup.Add(GetSurpriseExtraLerp())
```

## Sequences
You can also schedule lerps, so they will execute in succession. LerpDelays can also be added between lerp in LerpSequences 
```c#
_schedule = new LerpSequence(() => GetLerpToTarget(leftPosition), () => GetLerpToTarget(rightPosition));
// or
_schedule = new LerpSequence(() => Lerper.Delay(timeInMs), () => MoveBackToPosition());
```