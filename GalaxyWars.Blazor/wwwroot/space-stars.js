// Basit yıldız animasyonu
const STAR_COUNT = 120;
const STAR_COLOR = 'rgba(255,255,255,0.85)';
const STAR_SIZE = 1.2;
const STAR_SPEED = 0.08;

let stars = [];

function randomStar(canvasWidth, canvasHeight) {
    return {
        x: Math.random() * canvasWidth,
        y: Math.random() * canvasHeight,
        z: Math.random() * canvasWidth,
    };
}

function createStars(canvas, count) {
    stars = [];
    for (let i = 0; i < count; i++) {
        stars.push(randomStar(canvas.width, canvas.height));
    }
}

function drawStars(ctx, canvas, mouseX = 0, mouseY = 0) {
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    for (let i = 0; i < stars.length; i++) {
        let star = stars[i];
        let k = 128.0 / star.z;
        let sx = star.x * k + canvas.width / 2 + mouseX * 0.05;
        let sy = star.y * k + canvas.height / 2 + mouseY * 0.05;
        if (sx < 0 || sx >= canvas.width || sy < 0 || sy >= canvas.height) {
            // Reset star
            stars[i] = randomStar(canvas.width, canvas.height);
            continue;
        }
        let size = STAR_SIZE * (1 - star.z / canvas.width);
        ctx.beginPath();
        ctx.arc(sx, sy, size, 0, 2 * Math.PI);
        ctx.fillStyle = STAR_COLOR;
        ctx.shadowColor = '#fff';
        ctx.shadowBlur = 6;
        ctx.fill();
    }
}

function animateStars(canvas, ctx) {
    let mouseX = 0, mouseY = 0;
    canvas.addEventListener('mousemove', e => {
        mouseX = (e.clientX - canvas.width / 2) / 20;
        mouseY = (e.clientY - canvas.height / 2) / 20;
    });
    function animate() {
        for (let i = 0; i < stars.length; i++) {
            stars[i].z -= STAR_SPEED;
            if (stars[i].z <= 0) {
                stars[i] = randomStar(canvas.width, canvas.height);
                stars[i].z = canvas.width;
            }
        }
        drawStars(ctx, canvas, mouseX, mouseY);
        requestAnimationFrame(animate);
    }
    animate();
}

window.initSpaceStars = function() {
    const canvas = document.getElementById('space-stars-canvas');
    if (!canvas) return;
    function resize() {
        canvas.width = window.innerWidth;
        canvas.height = window.innerHeight;
        createStars(canvas, STAR_COUNT);
    }
    window.addEventListener('resize', resize);
    resize();
    const ctx = canvas.getContext('2d');
    animateStars(canvas, ctx);
};

// Otomatik başlatma (Blazor'da body yüklenince çağrılır)
if (document.readyState === 'complete' || document.readyState === 'interactive') {
    setTimeout(window.initSpaceStars, 100);
} else {
    window.addEventListener('DOMContentLoaded', window.initSpaceStars);
} 