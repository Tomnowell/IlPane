# Il Pane
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![standard-readme compliant](https://img.shields.io/badge/readme%20style-standard-brightgreen.svg?style=flat-square)](https://github.com/RichardLitt/standard-readme)
#### Video Demo:  https://youtu.be/FZ_YfSfx7fc
#### Description:
Bread recipe design application

## Table of Contents
- [Security](#security)
- [Background](#background)
- [Install](#install)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Security
If you worry about your security (and you should!) Please compile your own version with your own security certificate.  I do not promise to keep the binaries up to date, and you should be wary of anyone asking you to run powershell scripts, even a fellow bread baker.

## Background
This application is designed to simplify the creation and adaptation of bread recipes.

The software was in part inspired by the wonderful "BrewSmith" software developed by Brad Smith which assists amateur and professional brewers calculate recipes for beer (and other beverages, too).

While the scope of this application is less ambitious, I hope it can provide an easy and intuitive way to experiment and track recipes and improvements.  And in doing so, allows bakers to produce more tasty loafs with less hassle and less waste.


## Install
### Compile
Please compile your own version from the Microsoft Visual Studio .sln (solution), and source code files provided.

### Released binaries
Or check the [releases](https://github.com/Tomnowell/IlPane/releases) for the latest x86 release. Please run the Install.ps1 powershell script and allow Developer mode if not already enabled.  


## Usage
You can use the applications in two general ways.  These are:
- to hit a target loaf weight and hydration.
- to enter an existing recipe and then adjust the values.

**To understand the first paradigm,** consider this situation:
  You want to make a loaf of bread with the dough at a particular hydration without knowing the exact quantities of flour and water.  In this case, you can enter the 'Loaf ratio'* and the total weight along with the ratio or quantity of salt you want to add (and the weights of any other ingredients).  The applcation will then calculate the weights of flour and water for your recipe.
  
**The second paradigm,** consider:
  You have a recipe handed down from a dear relative who used to bake delicious bread, but did so by the metric tonne every day.  You really want to follow their recipe, but you don't have an oven large enough, and besides, you don't have enough friends to give that much bread to.  You can enter the weights of each ingredient.  Calculate the ratios and then adjust the recipe to keep the balance the same.
  
You can swap between these paradigms and once you're happy with the results you can name the recipe and save it.

### Save 
To save a recipe, enter a name and click the 'save' button.  Names must be unique.

### Load
To load a recipe, select the name of the recipe from the left list.  Then click the 'load' button.

### Delete
To delete a recipe select the name on the list on the left side, then click the 'delete' button'

### Clear All
Clear all will clear all text box values and resets the method of calculation to calculate by ratio (as default).  This does not affect any saved items unless the save button is clicked also.

### Clear Weights
Clears the text boxes that concern the bread's weight and sets the method of calculation to calculate by weight.

### Clear Ratios
Clears the text boxes that show the bread's ratios and resets the method of calculation to calculate by ratio.

### Exit
When you click the exit button it will ask you whether you want to save or not.  Then it will either save and exit or just exit depending on what option you pick.
```
Note: There are many ways to make a loaf of bread.  I hope I have covered all possible ways of trying to design a recipe, but if not it's a bug or an opportunity for developement.  Please let me know what you find!
```
## Contributing
PRs accepted.

Advice and baking tips also accepted!

### Thanks
Thanks to Eric Sink for SQLitePCLRaw package.
Thanks to Microsoft for Sqlite.Core and .NETCore.

## License
[MIT © Thomas Sion Nowell](https://github.com/Tomnowell/IlPane/blob/ca519b0f348ab84166fc656314b9dde0bf30e46f/LICENSE)

### Disclaimer
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.


