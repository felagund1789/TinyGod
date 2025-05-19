# 🌍 Tiny God

**Tiny God** is a minimalist god game prototype created for the **GameDev.tv Game Jam 2025**, where you play as a divine being watching over a tiny spherical planet. Shape the land, control the weather, and guide (or smite) the primitive creatures who call your world home.

---

## 🎮 Gameplay Overview

- Rotate your miniature planet to observe and interact with your world.
- Use divine powers like **Rain** to grow trees or **Fireball** to destroy them (and creatures).
- Watch as NPCs wander the land, surviving, thriving — or dying by your hand.
- A simple **faith system** governs your power — the more followers believe in you, the faster your abilities recharge.

---

## 🛠️ Features (Prototype)

- 🌐 Rotating spherical planet
- 🧍 Primitive AI creatures roaming the surface
- ⚡ God Powers:
    - **Rain** (grows vegetation)
    - **Fireball** (destroys trees and NPCs)
- 🔁 Simple faith meter system (prototype stage)

---

## 🎮 Controls

| Action                 | Input              |
|------------------------|--------------------|
| Rotate Planet          | Right Mouse Click  |
| Use Power              | Left Mouse Click   |
| Select Power: Rain     | Press `1`          |
| Select Power: Fireball | Press `2`          |

---

## 🧱 Project Structure
```plaintext
Assets/
├── AssetPacks/ (External assets)
├── Materials/
├── Prefabs/
│ ├── Farm.prefab
│ ├── Fireball.prefab
│ ├── Rain.prefab
│ ├── Tiny Worker.prefab
│ └── Tree.prefab
└── Scripts/
  ├── AbstractGrowable.cs
  ├── Fireball.cs
  ├── GodPowers.cs
  ├── NPCWalker.cs
  ├── PlanetController.cs
  ├── RainType.cs
  ├── Spawner.cs
  └── FaithManager.cs
```

---

## 📦 Dependencies

- Unity 6 (Built using the Legacy Input system)
- External assets:
  - [Environment Pack: Free Forest Sample by Supercyan](https://assetstore.unity.com/packages/p/environment-pack-free-forest-sample-168396)
  - [Free Fire VFX - URP by Vefects](https://assetstore.unity.com/packages/p/free-fire-vfx-urp-266226)
  - [Stylized Lava materials by Rob luo](assetstore.unity.com/packages/p/stylized-lava-materials-180943)

---

## 🚧 Future Plans

- More complex creature AI (tribes, building, reproduction)
- Terrain terraforming powers
- Civilization evolution and moral alignment system
- Faith-driven progression (more powers unlocked by belief)
- Optional rival gods or disasters

---

## 📜 License

This project is a prototype for learning and experimentation. Use and modify freely.

---

## 🙏 Credits

Made with ❤️ in Unity for the **GameDev.tv Game Jam 2025**. Prototype concept inspired by classic god games like *Black & White* and *Reus*.

