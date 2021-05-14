# Il pane
Bread recipe design application

## Introduction
This application is designed to simplify the creation and adaptation of bread recipes.

The software was in part inspired by the wonderful "BrewSmith" software developed by Brad Smith which assists amateur and professional brewers calculate recipes for beer (and other beverages, too).

While the scope of this application is less ambitious, I hope it can provide an easy and intuitive way to experiment and track recipes and improvements.  And in doing so, allows bakers to produce more tasty loafs with less hassle and less waste.

## Overview

You can use the applications in two general ways.  These are:
- to hit a target loaf weight and hydration.
- to enter an existing recipe and then adjust the values.

To understand the first paradigm, consider this situation:
  You want to make a loaf of bread with the dough at a particular hydration without knowing the exact quantities of flour and water.  In this case, you can enter the 'Loaf ratio'* and the total weight along with the ratio or quantity of salt you want to add (and the weights of any other ingredients).  The applcation will then calculate the weights of flour and water for your recipe.
  
The second paradigm, consider:
  You have a recipe handed down from a dear relative who used to bake delicious bread, but did so by the metric tonne every day.  You really want to follow their recipe, but you don't have an oven large enough, and besides, you don't have enough friends to give that much bread to.  You can enter the weights of each ingredient.  Calculate the ratios and then adjust the recipe to keep the balance the same.
  
You can swap between these paradigms and once you're happy with the results you can name the recipe and save it.

### Save 
To save a recipe, enter a name and click the 'save' button.  Names must be unique.

### Load
TO load a recipe, select the name of the recipe from the left list.  Then click the 'load' button.

## INSTALL instructions

### Compile
Please compile your own version from the Microsoft Visual Studio .sln (solution), and source code files provided.

### Released binaries
Or check the releases section for the latest x86 release. Please run the Install.ps1 powershell script and allow Developer mode if not already enabled.   

### WARNING Security WARNING
If you worry about your security (and you should!) Please compile your own version with your own security certificate.  I do not promise to keep the binaries up to date, and you should be wary of anyone asking you to run powershell scripts, even a fellow bread baker.

## Development
I decided on Universal Windows Platform for this project using XAML for UI and C# for program code.  I decided on these tools because they are recommended by Microsoft for developing UWP apps.  I thought using these tools would broaden my skillset to use a (new to me) coding language and markup.  

I would appreciate any suggestions from coders or bakers for how to improve this app.  In the future, I would like to make iOS and Android versions also because everyone needs bread dough sticky fingerprints on their phones and tablets!  I may do this with Xamarin, but I may use React or just go native and make native swift and java versions.  If you have a (reasonable) opinion, let me know!

## Enjoy baking
Please don't forget your love of baking even though this software may transform your life.  It may be super addictive to tweak your recipe until you have it just right.  I hope you find that perfect loaf.  But don't forget your love of getting your hands covered in flour, kneading that yeasty dough until your shoulders ache and then enjoying the wonderful scented crust of a freshly baked loaf.  Good luck & bon appetite!

## Thanks
Thanks to Eric Sink for SQLitePCLRaw package.
Thanks to Microsoft for Sqlite.Core and .NETCore

* "Baker's percentage" or "baker's math": every ingredient is expressed as a percentage of the flour weight (not total weight). The flour weight is expressed as 100%.  So Ratio = Water/Flour x 100.

## Disclaimer

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
