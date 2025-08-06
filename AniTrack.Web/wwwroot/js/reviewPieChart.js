function drawReviewPieChart(canvasId, total, recommended) {
    if (!total || total === 0) return;
    const notRecommended = total - recommended;
    const ctx = document.getElementById(canvasId).getContext('2d');
    const recommendedAngle = (recommended / total) * 2 * Math.PI;

    // Draw recommended slice
    ctx.beginPath();
    ctx.moveTo(80, 80);
    ctx.arc(80, 80, 60, 0, recommendedAngle);
    ctx.closePath();
    ctx.fillStyle = '#2ecc40';
    ctx.fill();

    // Draw not recommended slice
    ctx.beginPath();
    ctx.moveTo(80, 80);
    ctx.arc(80, 80, 60, recommendedAngle, 2 * Math.PI);
    ctx.closePath();
    ctx.fillStyle = '#e74c3c';
    ctx.fill();

    // Add percentage text
    ctx.fillStyle = '#fff';
    ctx.font = 'bold 15px Arial';
    ctx.textAlign = 'center';
    ctx.fillText(Math.round((recommended/total)*100) + '% Recommended', 80, 90);
  }