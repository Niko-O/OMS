//region ScoreBoard

function showScoreboard()
{
    document.getElementById("scoreboard").style.visibility = "visible";
}

function hideScoreboard()
{
    document.getElementById("scoreboard").style.visibility = "hidden";
}

function setPlayerNames(playerOne, playerTwo)
{
    document.getElementById("name_one").innerHTML = playerOne;
    document.getElementById("name_two").innerHTML = playerTwo;
}

function setPlayerNameOne(playerOne)
{
    document.getElementById("name_one").innerHTML = playerOne;
}

function setPlayerNameTwo(playerTwo)
{
    document.getElementById("name_Two").innerHTML = playerTwo;
}

function setStandings(setsOne, gamesOne, pointsOne, setsTwo, gamesTwo, pointsTwo)
{
    document.getElementById("sets_one").innerHTML = setsOne;
    document.getElementById("games_one").innerHTML = gamesOne;
    document.getElementById("points_one").innerHTML = pointsOne;
    document.getElementById("sets_one").innerHTML = setsTwo;
    document.getElementById("games_one").innerHTML = gamesTwo;
    document.getElementById("points_one").innerHTML = pointsTwo;
}

function setStandingsPlayerOne(setsOne, gamesOne, pointsOne)
{
    document.getElementById("sets_one").innerHTML = setsOne;
    document.getElementById("games_one").innerHTML = gamesOne;
    document.getElementById("points_one").innerHTML = pointsOne;
}

function setStandingsPlayerTwo(setsTwo, gamesTwo, pointsTwo)
{
    document.getElementById("sets_one").innerHTML = setsTwo;
    document.getElementById("games_one").innerHTML = gamesTwo;
    document.getElementById("points_one").innerHTML = pointsTwo;
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

var lowerThird_main = document.getElementById("main");
var checkMain = true;
var checkSub = true;

function inSubLowerThird()
{
    if (checkMain)
    {
        document.getElementById("sub").classList.add("flyIn");
        checkMain = false;
    }
}

function outMainLowerThird()
{
    if (checkSub)
    {
        document.getElementById("main").classList.remove("flyIn");
        checkSub = false;
    }
}

function showLowerThird()
{
    /*document.getElementById("lower_third").style.visibility = "visible";*/
    checkMain = true;
    document.getElementById("main").classList.add("flyIn");
    document.getElementById("main").addEventListener("transitionend", inSubLowerThird, false);
}

function hideLowerThird()
{
    //document.getElementById("lower_third").style.visibility = "hidden";
    checkSub = true;
    document.getElementById("sub").classList.remove("flyIn");
    document.getElementById("sub").addEventListener("transitionend", outMainLowerThird, false);
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

function showCrawlerTop()
{
    document.getElementById("crawler_top").style.visibility = "visible";
}

function hideCrawlerTop()
{
    document.getElementById("crawler_top").style.visibility = "hidden";
}

function setCrawlerTopText(text)
{
    document.getElementById("crawler_top").innerHTML = text;

}

//endregion

//region CrawlerDown

function showCrawlerBottom()
{
    document.getElementById("crawler_bottom").style.visibility = "visible";
}

function hideCrawlerBottom()
{
    document.getElementById("crawler_bottom").style.visibility = "hidden";
}

function setCrawlerBottomText(text)
{
    document.getElementById("crawler_bottom").innerHTML = text;
}

//endregion