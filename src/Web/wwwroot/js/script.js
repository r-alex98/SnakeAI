let canvasHeight;
let canvasWidth;
let gridSize;
let tileSizeInPixels;

const backgroundColor = "rgba(255, 255, 255, 1)";
const bodyColor = "rgba(0, 0, 0, 1)";
const foodColor = "rgba(255, 0, 0, 1)";
const bordersColor = "rgba(102, 255, 51, 0.5)";

function createGrid(count, height, width, boardGridSize, tileSize){
    canvasHeight = height;
    canvasWidth = width;
    gridSize = boardGridSize;
    tileSizeInPixels = tileSize;
    
    const canvasContainer = document.getElementById("canvas-container");
    canvasContainer.innerHTML = "";
    for (let i = 0; i < count; i++){
        let canvas = createCanvas(i);
        canvasContainer.append(canvas);
    }
}

function createCanvas(canvasId){
    let canvas = document.createElement('canvas');
    canvas.id = canvasId.toString();
    canvas.height = canvasHeight;
    canvas.width = canvasWidth;
    return canvas;
}

function drawTile(canvasId, x, y, color){
    const can = document.getElementById(canvasId.toString());
    const ctx = can.getContext("2d");
    ctx.fillStyle = color;
    ctx.fillRect(x * tileSizeInPixels, y * tileSizeInPixels, tileSizeInPixels, tileSizeInPixels);
}

function clearCanvas(canvasId){
    const can = document.getElementById(canvasId.toString());
    const ctx = can.getContext("2d");
    ctx.fillStyle = backgroundColor;
    ctx.fillRect(0, 0, can.width, can.height);
}

function drawBodyTile(canvasId, x, y){
    drawTile(canvasId, x, y, bodyColor);
}

function drawFoodTile(canvasId, x, y){
    drawTile(canvasId, x, y, foodColor);
}

function drawGameBoard(canvasId, headX, headY, bodyX, bodyY, foodX, foodY){
    clearCanvas(canvasId);
    drawBodyTile(canvasId, headX, headY);
    for (let i = 0; i < bodyX.length; i++){
        drawBodyTile(canvasId, bodyX[i], bodyY[i]);
    }
    drawFoodTile(canvasId, foodX, foodY);
}

function highlightBorders(canvasIds){
    for(let i = 0; i < canvasIds.length; i++){
        drawBorders(canvasIds[i], bordersColor);
    }
}

function drawBorders(canvasId, color){
    let lastIndex = gridSize - 1;
    for (let i = 0; i < gridSize; i++){
        for (let j = 0; j < gridSize; j++){
            if (i === 0 || j === 0 || i === lastIndex || j === lastIndex)
                drawTile(canvasId, i, j, color);
        }
    }
}

function updateState(generation, score){
    document.getElementById("generation").innerText = generation.toString();
    document.getElementById("score").innerText = score.toString();
}
