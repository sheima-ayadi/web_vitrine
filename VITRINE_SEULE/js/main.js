// ─── PAGE NAVIGATION ───
function showPage(id) {
  document.querySelectorAll('.page').forEach(p => p.classList.remove('active'));
  document.querySelectorAll('.nav-links a').forEach(a => a.classList.remove('active'));
  const page = document.getElementById('page-' + id);
  if (page) {
    page.classList.add('active');
    window.scrollTo({ top: 0, behavior: 'smooth' });
    const nav = document.getElementById('nav-' + id);
    if (nav) nav.classList.add('active');
    else {
        const servicesNav = document.getElementById('nav-services');
        if (servicesNav) servicesNav.classList.add('active');
    }
  }
  initFadeIn();
}

// ─── HAMBURGER ───
function toggleMenu() {
  const mobileMenu = document.getElementById('mobileMenu');
  if (mobileMenu) {
    mobileMenu.classList.toggle('open');
  }
}

// ─── FADE-IN OBSERVER ───
function initFadeIn() {
  setTimeout(() => {
    const activePage = document.querySelector('.page.active');
    if (!activePage) return;
    
    const els = activePage.querySelectorAll('.fade-in');
    if ('IntersectionObserver' in window) {
      const obs = new IntersectionObserver((entries) => {
        entries.forEach(e => { 
          if (e.isIntersecting) { 
            e.target.classList.add('visible'); 
            obs.unobserve(e.target); 
          } 
        });
      }, { threshold: 0.1 });
      els.forEach(el => obs.observe(el));
    } else {
      els.forEach(el => el.classList.add('visible'));
    }
  }, 50);
}

// ─── COUNTER ANIMATION ───
function animateCounters() {
  document.querySelectorAll('.stat-num,.num').forEach(el => {
    const target = parseInt(el.textContent);
    if (isNaN(target)) return;
    const suffix = el.textContent.replace(/[0-9]/g,'');
    let current = 0;
    const step = target / 40;
    const timer = setInterval(() => {
      current = Math.min(current + step, target);
      el.textContent = Math.floor(current) + suffix;
      if (current >= target) clearInterval(timer);
    }, 30);
  });
}

// ─── FORM SUBMIT ───
function handleSubmit() {
  const btn = document.querySelector('#page-contact .btn-primary');
  if (btn) {
    btn.textContent = '✅ Message envoyé !';
    btn.style.background = 'linear-gradient(135deg,#00c888,#0077ff)';
    setTimeout(() => {
      btn.textContent = 'Envoyer ma Demande →';
      btn.style.background = '';
    }, 3000);
  }
}

// ─── INIT ───
document.addEventListener('DOMContentLoaded', () => {
  initFadeIn();
  animateCounters();
});

// loadBlogPosts supprimé pour version VITRINE SEULE
