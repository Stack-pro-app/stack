package com.ProjectMana.ProjectManagementSpring.repo;

import com.ProjectMana.ProjectManagementSpring.enteties.UserT;
import jakarta.persistence.criteria.CriteriaBuilder;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Component;

@Component
public interface UserRepo extends JpaRepository<UserT, Integer> {

}
