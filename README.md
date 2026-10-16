# Brant — Custom Character Mod for Slay the Spire 2 (WIP)

An extensible, custom-character mod for *Slay the Spire 2*, developed in **C# (.NET 9)** and **Godot 4.x** utilizing **BaseLib**. 
---

### Overview & Key Mechanics

Brant introduces a high-tempo, calculation-driven playstyle built around **Energy Recharge (ER)** and combat momentum:

- **Energy Recharge (ER) Resource System**:
  - Starts each combat round at a baseline (100 ER) up to a 250 ER cap.
  - Dynamically scales damage, block output, and card secondary effects based on current ER levels.
- **Bravo Relic & Gauge Loop**:
  - Automatically tracks basic attack sequences (every 2 attacks trigger healing and gauge progress).
  - Rewards players with bonus Energy and ER at milestone thresholds.
  - Generates the ultimate reward card, **Ocean Odyssey**, upon reaching maximum gauge capacity.
- **Synergistic Deck Pool**:
  - **10+ Custom Cards**: Including multi-hit scaling attacks (`All Out!`), risk-reward finishers (`Final Bow`), defensive mitigation (`Anchors Aweigh!` with damage-reduction `Flight`), and tactical setup skills.
  - Fully integrated card upgrades, custom keyword tooltips, dynamic variable calculation (`DynamicVar`), and localized string configurations (`cards.json`, `powers.json`, `keywords.json`).

---

### 🛠️ Technical Highlights & Architecture

- **Language & Runtime**: C# 13, .NET 9.0
- **Game Engine & Frameworks**: Godot Engine 4.5.x (.NET), Slay the Spire 2 Core API, BaseLib
- **Design Patterns & Architecture**:
  - **Custom Combat Hooks & State Management**: Intercepts round start, turn changes, and attack triggers using decoupled combat hooks (`ERSystem`).
  - **Object-Oriented Card Hierarchy**: Inherits from core models with clean encapsulation of card variables (`BlockVar`, `DamageVar`, `PowerVar`).
  - **Localization & Assets**: Data-driven design separating gameplay logic from assets and localization dictionaries.

---

### 🚀 Getting Started / Installation

1. Ensure **Slay the Spire 2** and a compatible mod loader / BaseLib are installed.
2. Build the project:
   ```bash
   dotnet build -c Release
