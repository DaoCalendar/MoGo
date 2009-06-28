# MoGo

## MoGo (pronounced mojo) is a free open source genetic optimiser add-on for NinjaTrader. 

It is generally capable of finding desirable strategy parameter combinations in a small fraction of the time it takes to exhaustively search the parameter space as NinjaTrader's default optimiser does.
 
It is a nearly full rewrite of Pete S.'s genetic optimiser (thanks Pete!) with improved GUI and added features, and supports all the original options including:
Selectable population size, number of generations, top % to reproduce, and mutation rate
User-specified minimum number of trades
Van Tharp's system quality number optimisation type
 
### Additional features:
* Supports all standard & custom optimisation types available to NinjaTrader
* Abitrary user-specified dynamic constraints on strategy parameters
* Progress dialog showing iteration number & true remaining time (NinjaTrader's progress dialog has trouble here)
* Detects with high probability when it has exhaused the valid parameter space
* Saves last-used settings between sessions
* Honours parameter minimum, maximum, and increment values

## Install MoGo

[Click here](http://cloud.github.com/downloads/celeretaudax/MoGo/MoGo_v1.1.zip) to install MoGo right away.


## Basic Usage

The installer adds additional options to your **Optimizer** and **Optimize on...** settings when configuring an optimisation run in NinaTrader's strategy manager. In order to use MoGo to optimise your strategies, select the MoGo optimiser (figure 1 below) and MoGo optimisation type (figure 2).
 
 

![Figure 1. Choosing the optimiser](http://i40.tinypic.com/2z3xnyh.png "Figure 1. Choosing the optimiser")

 

![Figure 2. Choosing the optimisation type](http://i39.tinypic.com/imo080.png "Figure 2. Choosing the optimisation type")

 
Enter other optimisation parameters as normal and click OK. The main MoGo window will appear as shown below:
 
 

![Figure 3. Main window](http://i40.tinypic.com/166lqvk.png "Figure 3. Main window")

### Maximum generations

Determines the number of times MoGo will create a population of parameter sets and evaluate them according to the fitness function. The initial population is randomly generated, while subsequent populations are descended from the best performers of the previous generation.
 
Larger numbers give MoGo more time to evolve an optimal solution, but take longer. The actual number of generations could be lower if MoGo runs out of new parameter combinations to try (at which point it will prompt asking if you wish to stop running - see below), or if the stop threshold is reached (see below).
 
### Population size

The number of individuals that are created for each generation. Larger numbers lead to a more diverse population (and hence better chance of finding an optimal solution) but also require more calculations.
 
### Reproduction rate

Specifies the top percentage of the population selected for reproducing once individuals have been ranked in order of fitness. I.e. 10 means the next generation is created from mating the top 10% of individuals in the current generation.
 
Setting this value too low can lead to a lack of diversity in the next generation, while setting it too high reduces the effect of natural selection that is generally required to evolve better solutions.
 
### Mutation rate

Specifies the percentage of genes (parameters) that are susceptible to mutation when parents mate to produce offspring. I.e. 5 means that offspring have a 5% chance of each of their parameters being randomly mutated from the corresponding value in either parent.
 
### Stop threshold

If an individual scores higher than this threshold on the fitness function (as well as meeting other required criteria such as minimum number of trades and conditions), MoGo will stop optimising. This can be useful for quickly finding an acceptable set of parameters that satisfy a minimum fitness requirement.
 
Setting this value to 0 disables the threshold.
 
### Save all trials

Checking this checkbox causes MoGo to write out all tested parameter combinations along with their fitness function results to a time-stamped CSV file in **My Documents\NinjaTrader 6.5\**. You can then manipulate the data as desired in a spreadsheet such as Excel.
 
### Fitness function

This is where you select the function (optimisation type) that MoGo will use to evaluate individuals. All the standard NinaTrader optimisation types are available, plus **Van Tharp's System quality number** as implemented by Pete S. in his original genetic optimiser.
 
### Minimum trades

Specifies the minimum number of trades that must occur using an individual's parameter settings in order for that individual to be considered viable.
 
Setting this too low increases the likelihood of curve-fitting, while setting it too high is likely to remove fit individuals from the gene pool. Reasonable values depend on the length of your backtest and time frame of your strategy; I wouldn't use anything under 50 myself.
 
##Strategy parameter constraints grid

Here you may enter any number of additional dynamic constraints on your strategy parameters using one C# boolean expressions per grid line. MoGo will then avoid creating individuals that violate any of the specified constraints.
 
For example, if your strategy has ProfitTargetTicks and StopLossTicks parameters and you want your profit target to be at least twice your stop loss, you could add the constraint:
 
`ProfitTargetTicks >= 2 * StopLossTicks`
 
 
Or to ensure they are never more than 5 ticks apart:
 
`Math.Abs(ProfitTargetTicks - StopLossTicks) <= 5`
 
 
Similarly for a MACD crossover strategy you may wish to ensure that your fast moving average period is less than your slow period. Assuming appropriately named parameters, this does the trick:
 
## FastPeriod < SlowPeriod
 
 
You may enter any valid C# boolean expression as a constraint (incuding using boolean operators like **&&**, **||**, **^**, etc.), but the error reporting is very limited due to the complexity under the hood, so try to keep them simple.
 
If you enter an invalid expression, a message to that effect appears below the grid and you will have to correct it before you can continue.
 
## Additional functionality

### Duplicate avoidance

MoGo keeps a record of every individual it has evaluated, and will never create an identical individual in the same optimisation run. This avoids wasting time re-evaluating the same parameter settings.
 
### Dead-end detection

MoGo also counts the number of consecutive attempts required to create each new individual. If it cannot find a new valid individual in a million attempts (e.g. because each new individual has already been evaluated, or does not satisfy the parameter constraints specified), it will ask you whether it should stop looking.
 
At this point it is likely that MoGo has exhausted the available parameter space, and you can fairly confidently stop. If you choose not to, MoGo will reset the attempt counter to 0 and try again. 
 
### Progress dialog

While optimising, MoGo displays a small progress dialog (figure 4) showing the current iteration number / total iterations, and remaining time. This time estimate is far more accurate than NinjaTrader's own progress dialog, which seems to have problems accurately estimating remaining time for any custom optimiser.
 
 

![Figure 4. MoGo's progress dialog](http://i42.tinypic.com/33cb21t.png "Figure 4. MoGo's progress dialog")

 
 
### Improved UI experience for walk-forward optimisation / frequent re-testing

Normally, MoGo displays its main dialog at the start of an optimisation run. This is not ideal for walk-forward optimisations as it will pop up at the beginning of each optimisation period, and the optimisation will not proceed until **OK** is manually clicked.
 
Holding down the **Ctrl** key when clicking **OK** tells MoGo not to display its dialog again until the **Ctrl** key is held down at a point when the dialog would normally pop up or NinjaTrader is restarted. 
 
### Persistent optimiser settings

MoGo remembers your genetic optimiser settings, fitness function selection, and parameter constraints between NinjaTrader runs, saving you from re-entering them each time you restart NinjaTrader (one of my main gripes about the current strategy analyzer).
 
Note that if you select a different strategy to optimise, any parameter constraints that are no longer valid for the selected strategy (because it does not have the same parameters as the constraint requires) will be removed from the grid automatically.