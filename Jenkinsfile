pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                sh 'docker compose build'
            }
        }
        stage('Run') {
            steps {
                echo 'docker compose up -d'
            }
        }  
        stage('Test') {
            steps {
                echo 'docker compose test'
            }
        }
        stage('Deliver') {
            steps {
                withCredentials([usernamePassword(credentialsId: 'e96d1be8-f79f-416b-871d-799c2b5eaa6c', usernameVariable: 'DOCKER_USER', passwordVariable: 'DOCKER_PASS')]) {
                sh 'docker login -u $DOCKER_USER -p $DOCKER_PASS'
                sh 'docker compose push'
            }
        }
    }
}