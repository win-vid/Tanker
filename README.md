<img width="1920" height="437" alt="TANKER" src="https://github.com/user-attachments/assets/a7a2b7ea-ea99-4ecf-b9d9-013b44f9a0b7" />
In Tanker, the world lies in ruins, consumed by a ruthless, misanthropic super-intelligence. As the last human tank commander, you are humanity’s final line of defense — a lone steel titan waging war against endless legions of AI-driven machines. Every kill secures a heartbeat for humanity.

<h2>About</h2>
Tanker is an endless isometric, single-player, bullet hell video-game in which you control a tank shooting down hordes of enemies. You are able to use power-ups to your advantage to rain even more carnage upon the deadly enemy war-machines.
This project is part of my introduction to Game Engines course. 

<h2>How to play</h2>
<h3>Movement</h3>
<ul>
  <li>W, Accelerate</li>
  <li>S, Drive Backwards</li>
  <li>A, Turn Left</li>
  <li>D, Turn Right</li>
</ul>

<h3>Shooting</h3>
<ul>
  <li>Turn the canon by moving the mouse cursor</li>
  <li>Press or hold the <b>left mouse button</b> to shoot</li>
</ul>

<h2>Enemies</h2>
The enemies are all based upon a small state machine which uses steering behavior to drive towards random points around the player.
<h3>TVP</h3>
A standard issue FPV drone. Very weak but fast. Flies towards a point, charges at the player, explodes.
<h3>T1000</h3>
Weakest enemy, can only shoot once and then has to drive towards the next point.
<h3>T2000</h3>
Constantly aims its turret at the player. Persistently shoots at the player.
<h3>T3000</h3>
Slow mini-boss, constantly aims its turret at the player and shoots disk projectiles which burst into multiple enemy projectiles after a short amount of time. Additionaly, possesses a small auto-canon at the front.
<h3>T4000</h3>
Slow mini-boss, has no weapons but can deploy TVP's.
<h3>TRX</h3>
Every 10th wave the TRX will appear. It has a set of turrets and can shoot srockets at the player. Its the most powerful enemy but also the slowest.
