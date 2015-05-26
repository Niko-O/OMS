
//region ScoreBoard

function showScoreboard()
{
    document.getElementById("scoreboard").style.visibility = "visible";
}

function hideScoreboard()
{
    document.getElementById("scoreboard").style.visibility = "hidden";
}

function setTeamNameOne(name)
{
    document.getElementById("name_one").innerHTML = name;
}

function setTeamNameTwo(name)
{
    document.getElementById("name_two").innerHTML = name;
}

function setPointsOne(points)
{
    document.getElementById("points_one").innerHTML = points;
}

function setPointsTwo(points)
{
    document.getElementById("points_two").innerHTML = points;
}

function setGamesOne(games)
{
    document.getElementById("games_one").innerHTML = games;
}

function setGamesTwo(games)
{
    document.getElementById("games_two").innerHTML = games;
}

function setSetsOne(sets)
{
    document.getElementById("sets_one").innerHTML = sets;
}

function setSetsTwo(sets)
{
    document.getElementById("sets_two").innerHTML = sets;
}

//endregion

//region LowerThird

function showLowerThird()
{
    document.getElementById("lower_third").style.visibility = "visible";
}

function hideLowerThird()
{
    document.getElementById("lower_third").style.visibility = "hidden";
}

function setLowerThirdTitleText(text)
{
    document.getElementById("title").innerHTML = text;
}

function setLowerThirdSubtitleText(text)
{
    document.getElementById("subtitle").innerHTML = text;
}

//endregion

//region CrawlerTop

function showCrawlerTop(){
    document.getElementById("crawler_top").style.visibility = "visible";
}

function hideCrawlerTop(){
    document.getElementById("crawler_top").style.visibility = "hidden";
}

function setCrawlerTopText(text)
{
    document.getElementById("crawler_top").innerHTML = text;

}

//endregion

//region CrawlerDown

function showCrawlerDown(){
    document.getElementById("crawler_down").style.visibility = "visible";
}

function hideCrawlerDown(){
    document.getElementById("crawler_down").style.visibility = "hidden";
}

function setCrawlerDownText(text)
{
    document.getElementById("crawler_down").innerHTML = text;
}

//endregion