var lowerThirdMain = null;
var lowerThirdSub = null;

function initialize()
{
    lowerThirdMain = document.getElementById("main");
    lowerThirdMain.addEventListener("transitionend", lowerThirdMainTransitionEnded, false);
    lowerThirdSub = document.getElementById("sub");
    lowerThirdSub.addEventListener("transitionend", lowerThirdSubTransitionEnded, false);
}

//region Sequences

var waitForMain = 1;
var waitForSub = 2;

var currentSequence = null;
var sequenceIndex = -1;

function startSequence(sequence)
{
    if (currentSequence != null) return;
    currentSequence = sequence;
    sequenceIndex = 0;
    runSequence();
}

function runSequence()
{
    while (sequenceIndex < currentSequence.length)
    {
        var item = currentSequence[sequenceIndex];
        if (item === waitForMain || item === waitForSub)
        {
            return;
        }
        else
        {
            item();
        }
        sequenceIndex++;
    }
    currentSequence = null;
    sequenceIndex = -1;
}

function lowerThirdMainTransitionEnded()
{
    if (currentSequence == null) return;
    if (currentSequence[sequenceIndex] === waitForMain)
    {
        sequenceIndex++;
        runSequence();
    }
}

function lowerThirdSubTransitionEnded()
{
    if (currentSequence == null) return;
    if (currentSequence[sequenceIndex] === waitForSub)
    {
        sequenceIndex++;
        runSequence();
    }
}

//endregion

//region ScoreBoard

var serveNone = 0;
var servePlayerOne = 1;
var servePlayerTwo = 2;

function showScoreboard()
{
    document.getElementById("scoreboard").classList.add("showIn");
}

function hideScoreboard()
{
    document.getElementById("scoreboard").classList.remove("showIn");
}

function setPlayerNames(playerOne, playerTwo)
{
    document.getElementById("nameOne").innerHTML = playerOne;
    document.getElementById("nameTwo").innerHTML = playerTwo;
}

function setStandings(setsOne, gamesOne, pointsOne, setsTwo, gamesTwo, pointsTwo)
{
    document.getElementById("setsOne").innerHTML = setsOne;
    document.getElementById("gamesOne").innerHTML = gamesOne;
    document.getElementById("pointsOne").innerHTML = pointsOne;
    document.getElementById("setsTwo").innerHTML = setsTwo;
    document.getElementById("gamesTwo").innerHTML = gamesTwo;
    document.getElementById("pointsTwo").innerHTML = pointsTwo;
}

function serve(player)
{
    switch (player)
    {
        case serveNone:
            document.getElementById("serveBox").style.visibility = "hidden";
            break;
        case servePlayerOne:
            document.getElementById("serveBox").style.visibility = "visible";
            document.getElementById("serveBox").classList.remove("moveIn");
            break;
        case servePlayerTwo:
            document.getElementById("serveBox").style.visibility = "visible";
            document.getElementById("serveBox").classList.add("moveIn");
            break;
    }
}

//endregion

//region LowerThird

function showLowerThird()
{
    startSequence([
        function() {document.getElementById("main").classList.add("flyIn");},
        waitForMain,
        function() {document.getElementById("sub").classList.add("flyIn");},
        waitForSub
    ]);
}

function hideLowerThird()
{
    startSequence([
        function() {document.getElementById("sub").classList.remove("flyIn");},
        waitForSub,
        function() {document.getElementById("main").classList.remove("flyIn");},
        waitForMain
    ]);
}

function setLowerThirdText(title, subtitle)
{
    document.getElementById("title").innerHTML = title;
    document.getElementById("subtitle").innerHTML = subtitle;

}

//endregion

//region CrawlerTop

function showCrawlerTop()
{
    //document.getElementById("crawler_top").style.visibility = "visible";
    document.getElementById("crawlerTopPack").classList.add("setVisible");
    document.getElementById("crawlerTopMoveD").classList.add("moveDown");
    document.getElementById("crawlerTopMoveU").classList.add("moveUp");
}

function hideCrawlerTop()
{
    //document.getElementById("crawler_top").style.visibility = "hidden";
    document.getElementById("crawlerTopPack").classList.remove("setVisible");
    document.getElementById("crawlerTopMoveD").classList.remove("moveDown");
    document.getElementById("crawlerTopMoveU").classList.remove("moveUp");
}

//endregion

//region CrawlerDown

function showCrawlerBottom()
{
    document.getElementById("crawlerBottomPack").classList.add("setVisible");
    document.getElementById("crawlerBottomMoveD").classList.add("moveDown");
    document.getElementById("crawlerBottomMoveU").classList.add("moveUp");
}

function hideCrawlerBottom()
{
    document.getElementById("crawlerBottomPack").classList.remove("setVisible");
    document.getElementById("crawlerBottomMoveD").classList.remove("moveDown");
    document.getElementById("crawlerBottomMoveU").classList.remove("moveUp");
}

function setCrawlerText(text){
    document.getElementById("crawlerTopText").innerHTML = text;
    document.getElementById("crawlerBottomText").innerHTML = text;
}

//endregion