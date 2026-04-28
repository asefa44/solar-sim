# 🌍 Solar System Simulation

A physically accurate solar system simulation built in **Unity**, where all planetary motion is governed by **Newton's Law of Universal Gravitation** and integrated using the **4th-order Runge-Kutta (RK4)** method.

![Unity](https://img.shields.io/badge/Unity-2022+-black?logo=unity) ![Physics](https://img.shields.io/badge/Physics-Newtonian-blue) ![License](https://img.shields.io/badge/License-MIT-green)

---

## 🔭 Overview

This project simulates the motion of all 8 planets in our solar system. Rather than using pre-defined orbital paths or animations, every planet's trajectory emerges purely from **gravitational forces** calculated at each simulation step. The result is a dynamic n-body simulation where planets influence each other's orbits — just as they do in reality.

---

## ⚙️ Physics Model

### Newton's Law of Universal Gravitation

Every pair of bodies exerts a gravitational force on each other:

$$F = G \frac{M \cdot m}{r^2}$$

| Symbol | Description |
|--------|-------------|
| `G` | Gravitational constant (scaled to simulation units: `0.091`) |
| `M` | Mass of the attracting body |
| `m` | Mass of the attracted body |
| `r` | Distance between the two bodies |

Since we work with accelerations directly, mass `m` cancels out:

$$a = \frac{G \cdot M}{r^2}$$

This is computed for every pair of bodies at each simulation step — **72 calculations per step** for 9 bodies (8 planets + Sun).

---

### N-Body Problem

Unlike simplified two-body simulations, this project implements a full **n-body gravitational simulation**. Every planet attracts every other planet. This means:

- Jupiter's gravity slightly perturbs Mars's orbit
- Venus and Earth influence each other during conjunctions
- The Sun dominates but is not the only gravitational source

This produces physically realistic orbital perturbations — the same phenomenon NASA accounts for in real mission planning.

---

### RK4 Numerical Integration

Newton's law gives us acceleration. To get velocity and position, we must integrate:

$$a \xrightarrow{\int} v \xrightarrow{\int} x$$

Simple **Euler integration** accumulates errors quickly, causing planets to spiral outward. Instead, this simulation uses **4th-order Runge-Kutta (RK4)**, which samples 4 points per step and takes a weighted average:

$$x_{n+1} = x_n + \frac{\Delta t}{6}(k_1 + 2k_2 + 2k_3 + k_4)$$

| Sample | Description | Weight |
|--------|-------------|--------|
| k1 | Slope at step start | 1x |
| k2 | Slope at step midpoint (via k1) | 2x |
| k3 | Slope at step midpoint (via k2) | 2x |
| k4 | Slope at step end | 1x |

Midpoints are weighted more heavily because they better represent the curve's behavior between steps.

Additionally, each Unity `FixedUpdate` is divided into **20 substeps**, further reducing accumulated error.

---

### Initial Velocity — Circular Orbit Condition

Each planet starts with the exact velocity needed for a circular orbit, derived by equating centripetal and gravitational forces:

$$\frac{mv^2}{r} = \frac{GMm}{r^2} \implies v = \sqrt{\frac{GM}{r}}$$

---

### Scaling System

Real astronomical values are scaled consistently to fit the simulation:

| Real World | Simulation |
|------------|------------|
| 1 AU | 10 units |
| 1 Earth day | 0.0986 seconds |
| 1 Earth year (365 days) | 36 seconds |

The gravitational constant `G` was derived from these constraints using Kepler's Third Law:

$$G = \frac{4\pi^2 r^3}{M \cdot T^2} = 0.091$$

---

## 🪐 Planetary Data

| Planet | Real Distance (AU) | Sim Distance (units) | Real Period (days) | Sim Period (seconds) |
|--------|--------------------|----------------------|--------------------|----------------------|
| Mercury | 0.387 | 3.87 | 88 | 8.7 |
| Venus | 0.723 | 7.23 | 225 | 22.2 |
| Earth | 1.000 | 10.00 | 365 | 36.0 |
| Mars | 1.524 | 15.24 | 687 | 67.8 |
| Jupiter | 5.203 | 52.03 | 4333 | 427.4 |
| Saturn | 9.537 | 95.37 | 10759 | 1060.9 |
| Uranus | 19.191 | 191.91 | 30687 | 3026.8 |
| Neptune | 30.069 | 300.69 | 60190 | 5934.7 |

---

## 🎮 Controls

| Input | Action |
|-------|--------|
| **Left Click** on planet | Focus camera on that planet |
| **Tab** | Cycle through planets |
| **Space** | Overview of full solar system |
| **Scroll Wheel** | Zoom in / out |
| **Right Click + Drag** | Orbit camera around focused planet |

---

## 📁 Project Structure

```
Assets/
├── Scripts/
│   ├── CelestialBody.cs       # Mass, velocity, orbit initialization
│   ├── GravityManager.cs      # RK4 integration, n-body gravity
│   ├── SolarSystemCamera.cs   # Camera controls, planet focus, hover
│   ├── OrbitTrail.cs          # Trail renderer for orbital paths
│   └── OrbitCounter.cs        # Measures orbital period (debug)
└── Scenes/
    └── SolarSystem.unity
```

---

## 🔬 Simulation Results

Orbital periods measured over a 1200-second test run:

| Planet | Target | Measured | Error |
|--------|--------|----------|-------|
| Mercury | 8.7s | 8.5–8.73s | ~2% |
| Venus | 22.2s | 20.5–22.42s | ~4% |
| Earth | 36.0s | 35.5–37.99s | ~3% |
| Mars | 67.8s | 68–71s | ~3% |
| Jupiter | 427.4s | 419–421s | ~2% |
| Saturn | 1060.9s | 1045.77s | ~1.5% |

Deviations are within expected range for an n-body simulation — planets mutually perturb each other's orbits, which is physically accurate behavior.

---

## 🛠️ Built With

- [Unity](https://unity.com/) — Game engine
- C# — Scripting
- Newtonian Mechanics — Physics model
- RK4 Integration — Numerical solver

---

## 📜 License

MIT License — feel free to use, modify and distribute.
