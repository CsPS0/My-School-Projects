solve_parabola <- function(a) {
  cat(sprintf("--- Egyenlet: y = %gx^2 ---\n", a))
  p <- 1 / (2 * abs(a))
  dist <- p / 2
  if (a > 0) {
    cat("Irány: Felfelé\n")
    cat(sprintf("Fókusz (F): (0; %g)\n", dist))
    cat(sprintf("Vezéregyenes (d): y = %g\n", -dist))
  } else {
    cat("Irány: Lefelé\n")
    cat(sprintf("Fókusz (F): (0; %g)\n", -dist))
    cat(sprintf("Vezéregyenes (d): y = %g\n", dist))
  }
  cat("\n")
}

solve_parabola(2)
solve_parabola(-2)
