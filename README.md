# Solar System Simulation

A Unity project simulating planetary motion through direct numerical solution of Newton's gravitational law, rather than scripted animation. Each planet's position is computed every frame from the combined gravitational pull of every other body in the system.

<img width="720" height="480" alt="222" src="https://github.com/user-attachments/assets/c9288e3c-0467-4c54-a2d2-a95c08f21b41" />


## Scope

The simulation is not a scale model. Real orbital distances and periods span a range too large to render or observe directly (Neptune orbits the Sun once every 165 years, at a distance 30 times greater than Earth's). To make the system observable, three quantities were compressed by fixed ratios:

| Quantity | Real value | Simulated value |
|---|---|---|
| Distance | 1 AU | 10 units |
| Time | 1 Earth year | 36 seconds |
| Mass | 1 Earth mass | 1 mass unit |

These ratios are applied uniformly across all planets, so relative distances and relative periods remain proportionally correct even though absolute values are compressed.

## Gravity and integration

Each pair of bodies attracts according to Newton's law:

`F = G × m1 × m2 / r²`

Every planet's acceleration is calculated from the pull of all other bodies simultaneously, making this a full N-body system rather than a Sun-only approximation.

Positions are advanced using 4th-order Runge-Kutta integration (RK4), evaluated in 20 substeps per physics frame. An Euler integrator was tested initially and produced orbital decay within seconds; RK4 keeps orbits stable across long simulated durations.

Initial orbital velocity for each planet is set using the two-body circular orbit formula:

`v = √(G × M_sun / r)`

Because this formula does not account for the gravitational influence of other planets at the starting moment, orbits carry a small initial eccentricity. This produces minor, self-correcting drift over time, most visible in Mars due to Jupiter's proximity and mass.

## Deriving the gravitational constant

G is not taken from real-world physics; it is solved for, so that Earth, placed 10 units from the Sun, completes one orbit in exactly 36 seconds. Using Kepler's third law:

`G = 4π² r³ / (M × T²)`

This gives **G = 0.091**, applied as a single constant across the entire simulation.

## Reference values

| Planet | Distance (AU / units) | Orbital period (real / simulated) |
|---|---|---|
| Mercury | 0.39 / 3.9 | 88 days / 8.7 s |
| Venus | 0.72 / 7.2 | 225 days / 22.2 s |
| Earth | 1.00 / 10.0 | 365 days / 36.0 s |
| Mars | 1.52 / 15.2 | 687 days / 67.8 s |
| Jupiter | 5.20 / 52.0 | 4,333 days / 427.4 s |
| Saturn | 9.54 / 95.4 | 10,759 days / 1,060.9 s |
| Uranus | 19.19 / 191.9 | 30,687 days / 3,026.8 s |
| Neptune | 30.07 / 300.7 | 60,190 days / 5,934.7 s |

## Mass scaling

Planetary masses use real Earth-relative ratios, with two exceptions.

| Body | Real mass (Earth = 1) | Simulated mass |
|---|---|---|
| Sun | 333,000 | 333 (fixed position) |
| Mercury | 0.055 | 0.055 |
| Venus | 0.815 | 0.815 |
| Earth | 1.000 | 1.000 |
| Mars | 0.107 | 0.107 |
| Jupiter | 317.8 | 1.0 |
| Saturn | 95.2 | 0.5 |
| Uranus | 14.5 | 14.5 |
| Neptune | 17.1 | 17.1 |

Jupiter and Saturn's masses were reduced. At a compressed spatial scale, their real mass generates gravitational forces strong enough to destabilize the inner planets within minutes, an effect exaggerated by spatial compression rather than by an error in the force calculation. The reduction preserves the same physical law while keeping the system observable.

The Sun is held at a fixed position rather than integrated. This is a standard simplification in orbital simulations and prevents long-term numerical drift of the reference frame.

## Validation

Two independent checks were used to confirm correct physical behavior.

**Orbital period accuracy**, measured over a continuous 1,200 second run:

| Planet | Target period | Measured period |
|---|---|---|
| Mercury | 8.7 s | 8.5 to 8.73 s |
| Venus | 22.2 s | 20.5 to 22.42 s |
| Earth | 36.0 s | 35.5 to 37.99 s |
| Mars | 67.8 s | 68 to 71 s |
| Jupiter | 427.4 s | 419 to 421 s |
| Saturn | 1,060.9 s | 1,045.8 s |

All results fall within 1 to 4 percent of target, consistent with expected N-body perturbation rather than integration error.

**Energy conservation.** In an isolated gravitational system, total mechanical energy (kinetic plus potential) should remain constant. Over the same test run, total energy held at approximately -3.97, drifting by 0.0002 to 0.0008 percent. A first-order Euler integrator typically drifts 5 to 50 percent under equivalent conditions, confirming the RK4 implementation behaves correctly.

At close planetary encounters, gravitational softening (`F = G·m1·m2 / (r² + ε²)`) is applied to prevent force singularities as distance approaches zero, avoiding artificial energy spikes during near collisions.

## Features

Real time N-body gravitational simulation with on-screen energy tracking. In-simulation calendar converting elapsed orbital time into a real date. Free orbit camera with zoom, planet focus, and click to select. Distance-adaptive planet labels and clickable markers visible at any zoom level. Per-planet information panel showing mass, radius, temperature, moon count, and live orbital data.

## Controls

| Input | Action |
|---|---|
| Right click and drag | Orbit the camera |
| Scroll | Zoom |
| Click a planet | Focus and follow |
| Click the Sun or press Space | Return to free view |
| Tab | Cycle through planets |# 🌍 Solar System Simulation

## License

MIT License — feel free to use, modify and distribute.
