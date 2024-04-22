pipeline {
    agent any
    
    stages {
        stage('Unit tests') {
            steps {
                sh 'dotnet test Backend/PS-Tests'
                sh 'dotnet test Backend/MS-Tests'
            }
        }
        stage('Build docker images') {
            steps {
                sh 'docker compose build'
            }
        }
        stage('Publish docker images') {
            steps {
                withCredentials([usernamePassword(credentialsId: 'Dockerhub', usernameVariable: 'DOCKER_USER', passwordVariable: 'DOCKER_PASS')]) 
                {
                    sh 'docker login -u $DOCKER_USER -p $DOCKER_PASS'
                    sh 'docker compose push'
                }
            }
        }
        stage('Deploy - fake') {
            steps {
                echo 'serve application'
            }
        }
    }
}
